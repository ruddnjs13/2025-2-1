using System.Collections.Generic;
using Code.Core;
using Code.Core.GameEvent;
using Code.Towers;
using DG.Tweening;
using Settings.InputSettings;
using UnityEngine;

namespace Code.Build
{
    public class BuildManager : MonoSingleton<BuildManager>
    {
        [SerializeField] private InputReaderSO inputReader;
        [SerializeField] private List<GameObject> towerPrefabs;
        [SerializeField] private GameEventChannelSO goldChannel;
        [SerializeField] private LayerMask whatIsPlaceTile;

        private const int MaxTowerCount = 3;
        private Camera _mainCam;
        
        private readonly Dictionary<TowerBase, PlaceTile> _tileDic = new();

        public TowerBase SelectedTower { get; private set; }
        public PlaceTile SelectedTile { get; private set; }

        private void Awake()
        {
            _mainCam = Camera.main;
        }

        private void OnEnable()
        {
            inputReader.OnDragEvent += HandleDragEvent;
        }

        #region Drag & Drop
        private void HandleDragEvent(bool isDragging)
        {
            if (isDragging)
            {
                SelectedTower = TrySelectTower();
                SetDragState(SelectedTower, true);
            }
            else
            {
                Drop();
            }
        }

        private void Drop()
        {
            if (SelectedTower == null) return;

            SelectedTile = TrySelectPlaceTile();
            
            if (SelectedTile == null || !SelectedTile.CanBuild || 
                !_tileDic.TryGetValue(SelectedTower, out var prevTile) || SelectedTile == prevTile)
            {
                Cancel();
                return;
            }

            SetDragState(SelectedTower, false);
            ProcessTowerInteraction(prevTile, SelectedTile, SelectedTower);
        }
        
        private void SetDragState(TowerBase tower, bool isDragging)
        {
            if (tower != null && _tileDic.TryGetValue(tower, out var tile))
            {
                foreach (TowerBase t in tile.ownTowerBase)
                {
                    if (isDragging) t.StartDrag();
                    else t.EndDrag();
                }
            }
        }

        
        #endregion

        #region Tower Interaction 

        private void ProcessTowerInteraction(PlaceTile from, PlaceTile to, TowerBase tower)
        {
            if (to.ownTowerBase.Count <= 0)
            {
                MoveTower(to, tower);
            }
            else
            {
                bool isSameType = tower.towerType == to.ownTowerBase[0].towerType;
                bool canMerge = isSameType && (to.ownTowerBase.Count + from.ownTowerBase.Count <= MaxTowerCount);

                if (canMerge) MergeTowers(from, to);
                else SwapTowers(from, to);
            }
        }

        public void MoveTower(PlaceTile nextTile, TowerBase tower)
        {
            if (!_tileDic.TryGetValue(tower, out var prevTile)) return;

            nextTile.PutTower(prevTile);
            prevTile.ClearTower();
    
            UpdateDicForTile(nextTile);
            UpdateDicForTile(prevTile);
        }

        private void MergeTowers(PlaceTile fromTile, PlaceTile toTile)
        {
            toTile.MergeTower(fromTile);

            fromTile.ClearTower();
            UpdateDicForTile(toTile);
            UpdateDicForTile(fromTile);
        }

        private void SwapTowers(PlaceTile tileA, PlaceTile tileB)
        {
            tileB.SwapTower(tileA);
    
            UpdateDicForTile(tileA);
            UpdateDicForTile(tileB);
        }

        public void BuildTower(PlaceTile placeTile, TowerType type, int spendAmount)
        {
            if (!placeTile.CanBuild || placeTile.ownTowerBase.Count >= MaxTowerCount) return;

            int typeIdx = (int)type;
            if (typeIdx < 0 || typeIdx >= towerPrefabs.Count) return;

            goldChannel.RaiseEvent(GoldEvent.spendGolEvent.Initialize(spendAmount));
            
            TowerBase tower = Instantiate(towerPrefabs[typeIdx]).GetComponent<TowerBase>();
            _tileDic[tower] = placeTile;
            placeTile.SetTower(tower);

            Transform spawnTrm = placeTile.PlaceTrm[placeTile.ownTowerBase.Count - 1];
            tower.transform.position = spawnTrm.position + new Vector3(0, 5f, 0);
            tower.transform.DOMoveY(spawnTrm.position.y, 0.2f)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() => tower.EnableTower());
        }
        
        #endregion
        
        #region Selection

        private void UpdateDicForTile(PlaceTile tile)
        {
            foreach (var tower in tile.ownTowerBase)
                _tileDic[tower] = tile;
        }
        
        private void Cancel()
        {
            if (SelectedTower != null && _tileDic.TryGetValue(SelectedTower, out var tile))
                tile.CancelMoveTower();
            
            SetDragState(SelectedTower, false);
            SelectedTower = null;
        }

        public TowerBase TrySelectTower()
        {
            Ray ray = _mainCam.ScreenPointToRay(inputReader.MousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _mainCam.farClipPlane))
                if (hit.collider.TryGetComponent(out TowerBase tower)) return tower;
            return null;
        }

        public PlaceTile TrySelectPlaceTile()
        {
            Ray ray = _mainCam.ScreenPointToRay(inputReader.MousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, whatIsPlaceTile))
                if (hit.collider.TryGetComponent(out PlaceTile placeTile)) return placeTile;
            return null;
        }

        #endregion
       
    }
}