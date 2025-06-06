using System;
using System.Collections.Generic;
using _01.Code.Tower.Towers;
using Core.GameEvent;
using DG.Tweening;
using Settings.InputSettings;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private List<GameObject> towerPrefabs;
    [SerializeField] private GameEventChannelSO goldChannel;

    [SerializeField] private LayerMask whatIsPlaceTile;
    
    private TowerBase _selectedTower;
    private PlaceTile _selectedTile;
    
    private Dictionary<TowerBase, PlaceTile> tileDic = new Dictionary<TowerBase, PlaceTile>();
    
    private void OnEnable()
    {
        inputReader.OnDragEvent += HandleDragEvent;
    }
    
    
    private void HandleDragEvent(bool isDrag)
    {
        if (isDrag)
        {
            _selectedTower = TrySelectTower();

            if (_selectedTower != null)
            {
                foreach (TowerBase ownTower in tileDic[_selectedTower]._ownTowerBase) 
                {
                    ownTower.StartDrag();
                }
            }
        }
        else
        {
            _selectedTile = TrySelectPlaceTile();


            if (_selectedTower != null && _selectedTile != null && _selectedTile.CanBuild)
            {
               
                PlaceTile prevTile = tileDic[_selectedTower];

                if (_selectedTile == prevTile)
                {
                    Cancel();
                    return;
                }

                if (_selectedTile._ownTowerBase.Count + prevTile._ownTowerBase.Count > 3)
                    Cancel();

                foreach (TowerBase ownTower in tileDic[_selectedTower]._ownTowerBase) 
                {
                    ownTower.EndDrag();
                }
                if (_selectedTile._ownTowerBase.Count <= 0)
                {
                    MoveTower(_selectedTile,_selectedTower);
                }

                else if (_selectedTower.towerType == _selectedTile._ownTowerBase[0].towerType)
                {
                    MergeTower();
                    prevTile.ClearTower();
                }
                else if (_selectedTower.towerType != _selectedTile._ownTowerBase[0].towerType)
                {
                    foreach (TowerBase tower in _selectedTile._ownTowerBase)
                    {
                        tileDic[tower] = prevTile;
                    }

                    foreach (TowerBase tower in prevTile._ownTowerBase)
                    {
                        tileDic[tower] = _selectedTile;
                    }
                    _selectedTile.SwapTower(prevTile);
                }
                
            }
            else if (_selectedTower != null)
            {
                Cancel();
            }
        }
    }

    private void Cancel()
    {
        foreach (TowerBase ownTower in tileDic[_selectedTower]._ownTowerBase) 
        {
            ownTower.EndDrag();
        }
        tileDic[_selectedTower].CancelMoveTower();
    }
    
    public void BuildTower(PlaceTile placeTile, TowerType type, int spendAmount)
    {
        if (placeTile.CanBuild)
        {
            goldChannel.RaiseEvent(GoldEvent.spendGolEvent.Initialize(spendAmount));
            TowerBase t = Instantiate(towerPrefabs[(int)type]).GetComponent<TowerBase>();
            
            tileDic.Add(t, placeTile);

            Transform spawnTrm = placeTile.PlaceTrm[placeTile._ownTowerBase.Count];
            placeTile.SetTower(t);
            t.transform.position = spawnTrm.position + new Vector3(0, 15f, 0);
            t.transform.DOMoveY(spawnTrm.position.y, 1f).SetEase(Ease.InOutCubic)
                .OnComplete(() =>
                {
                    t.EnableTower();
                });
        }
    }
    
    public void MoveTower(PlaceTile nextTile, TowerBase tower)
    {
        PlaceTile prevTile = tileDic[tower];
        if (nextTile.CanBuild)
        {
            if (nextTile.CanBuild)
            {
                nextTile.PutTower(prevTile);
                prevTile.ClearTower();
                tileDic[tower] = nextTile;
            }
        }
    }

    private void MergeTower()
    {
        foreach (TowerBase ownTower in tileDic[_selectedTower]._ownTowerBase) 
        {
            _selectedTile.MergeTower(ownTower);
            tileDic[ownTower] = _selectedTile;
        }
    }

    public TowerBase TrySelectTower()
    {
        Camera mainCam = Camera.main;
        Ray camRay = mainCam.ScreenPointToRay(inputReader.MousePosition);
        bool isHit = Physics.Raycast(camRay, out RaycastHit hit, mainCam.farClipPlane);
        if (isHit)
        {
            if (hit.collider.TryGetComponent(out TowerBase tower))
            {
                return tower;
            }
        }
        return null;
    }

    public PlaceTile TrySelectPlaceTile()
    {
        Camera mainCam = Camera.main;
        Ray camRay = mainCam.ScreenPointToRay(inputReader.MousePosition);
        bool isHit = Physics.Raycast(camRay, out RaycastHit hit, Mathf.Infinity, whatIsPlaceTile);
        if (isHit)
        {
            if (hit.collider.TryGetComponent(out PlaceTile placeTile))
            {
                return placeTile;
            }
        }
        return null;
    }
}
