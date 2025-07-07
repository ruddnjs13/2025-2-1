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

    private const int MaxTowerCount = 3;

    public TowerBase _selectedTower;
    public PlaceTile _selectedTile;

    private readonly Dictionary<TowerBase, PlaceTile> tileDic = new();

    private void OnEnable()
    {
        inputReader.OnDragEvent += HandleDragEvent;
    }

    private void HandleDragEvent(bool isDrag)
    {
        if (isDrag)
        {
            _selectedTower = TrySelectTower();
            SetDragState(_selectedTower, true);
        }
        else
        {
            _selectedTile = TrySelectPlaceTile();

            if (_selectedTower != null && _selectedTile != null && _selectedTile.CanBuild)
            {
                if (!tileDic.TryGetValue(_selectedTower, out var prevTile))
                {
                    Debug.LogWarning("선택된 타워가 타일딕에 없음.");
                    return;
                }

                if (_selectedTile == prevTile)
                {
                    Cancel();
                    return;
                }

                SetDragState(_selectedTower, false);

                if (_selectedTile._ownTowerBase.Count <= 0)
                {
                    MoveTower(_selectedTile, _selectedTower);
                }
                else if (_selectedTower.towerType != _selectedTile._ownTowerBase[0].towerType)
                {
                    SwapTowers(prevTile, _selectedTile);
                }
                else
                {
                    if (_selectedTile._ownTowerBase.Count + prevTile._ownTowerBase.Count > MaxTowerCount)
                    {
                        SwapTowers(prevTile, _selectedTile);
                    }
                    else
                    {
                        MergeTowers(prevTile, _selectedTile);
                        prevTile.ClearTower();
                    }
                }
            }
            else if (_selectedTower != null)
            {
                Cancel();
            }
        }
    }

    private void SetDragState(TowerBase tower, bool isDragging)
    {
        if (tower != null && tileDic.TryGetValue(tower, out var tile))
        {
            foreach (TowerBase t in tile._ownTowerBase)
            {
                if (isDragging) t.StartDrag();
                else t.EndDrag();
            }
        }
    }

    private void Cancel()
    {
        SetDragState(_selectedTower, false);
        if (tileDic.TryGetValue(_selectedTower, out var tile))
        {
            tile.CancelMoveTower();
        }
    }

    public void BuildTower(PlaceTile placeTile, TowerType type, int spendAmount)
    {
        if (!placeTile.CanBuild || placeTile._ownTowerBase.Count >= MaxTowerCount)
            return;

        if ((int)type < 0 || (int)type >= towerPrefabs.Count)
        {
            Debug.LogWarning("Invalid tower type index.");
            return;
        }

        goldChannel.RaiseEvent(GoldEvent.spendGolEvent.Initialize(spendAmount));
        TowerBase tower = Instantiate(towerPrefabs[(int)type]).GetComponent<TowerBase>();
        tileDic[tower] = placeTile;

        if (placeTile._ownTowerBase.Count >= placeTile.PlaceTrm.Length)
        {
            Debug.LogError("No available place transform for tower placement.");
            return;
        }

        Transform spawnTrm = placeTile.PlaceTrm[placeTile._ownTowerBase.Count];
        placeTile.SetTower(tower);

        tower.transform.position = spawnTrm.position + new Vector3(0, 5f, 0);
        tower.transform.DOMoveY(spawnTrm.position.y, 0.2f).SetEase(Ease.InOutCubic).OnComplete(() => tower.EnableTower());
    }

    public void MoveTower(PlaceTile nextTile, TowerBase tower)
    {
        if (!tileDic.TryGetValue(tower, out var prevTile)) return;
        if (!nextTile.CanBuild || nextTile._ownTowerBase.Count >= MaxTowerCount) return;

        nextTile.PutTower(prevTile);
        prevTile.ClearTower();

        foreach (TowerBase t in nextTile._ownTowerBase)
            tileDic[t] = nextTile;
    }

    private void MergeTowers(PlaceTile fromTile, PlaceTile toTile)
    {
        foreach (TowerBase tower in fromTile._ownTowerBase)
        {
            toTile.MergeTower(tower);
            tileDic[tower] = toTile;
        }
    }

    private void SwapTowers(PlaceTile tileA, PlaceTile tileB)
    {
        foreach (TowerBase tower in tileA._ownTowerBase)
        {
            tileDic[tower] = tileB;
        }
        foreach (TowerBase tower in tileB._ownTowerBase)
        {
            tileDic[tower] = tileA;
        }

        tileB.SwapTower(tileA);
    }

    public TowerBase TrySelectTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(inputReader.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Camera.main.farClipPlane))
        {
            if (hit.collider.TryGetComponent(out TowerBase tower))
                return tower;
        }
        return null;
    }

    public PlaceTile TrySelectPlaceTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(inputReader.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, whatIsPlaceTile))
        {
            if (hit.collider.TryGetComponent(out PlaceTile placeTile))
                return placeTile;
        }
        return null;
    }
}
