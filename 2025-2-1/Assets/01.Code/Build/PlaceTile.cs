using System.Collections.Generic;
using _01.Code.Tower.Towers;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlaceTile : MonoBehaviour
{
    [field: SerializeField] public Transform[] PlaceTrm { get; set; }
    
    public bool CanBuild { get; private set; } = true;
    
    [SerializeField] public List<TowerBase> _ownTowerBase = new List<TowerBase>(3);

    public void SetTower(TowerBase towerBase)
    {
        if (!CanBuild) return;

        _ownTowerBase.Add(towerBase);
        if (_ownTowerBase.Count >= 3)
            CanBuild = false;
    }

    public void MergeTower(TowerBase towerBase)
    {
        if (!CanBuild) return;
        _ownTowerBase.Add(towerBase);
        towerBase.transform.position = PlaceTrm[_ownTowerBase.Count-1].position;
    }

    public void ReNewTower()
    {
        int idx = 0;
        foreach (TowerBase tower in _ownTowerBase)
        {
            tower.transform.position = PlaceTrm[idx++].position;
        }
    }

    public void SwapTower(PlaceTile prevTile)
    {
        List<TowerBase> tmp = new List<TowerBase>(_ownTowerBase);
        _ownTowerBase = prevTile._ownTowerBase;
        prevTile._ownTowerBase = tmp;
        
        ReNewTower();
        prevTile.ReNewTower();
    }

    public void PutTower(PlaceTile prevTile)
    {
        if (!CanBuild) return;
        if (prevTile._ownTowerBase.Count == 1)
        {
            Debug.Log(prevTile);
            _ownTowerBase.Add(prevTile._ownTowerBase[0]);
            _ownTowerBase[0].transform.position = PlaceTrm[0].position;
        }
        else
        {
            int idx = 0;
            foreach (TowerBase tower in prevTile._ownTowerBase)
            {
                _ownTowerBase.Add(tower);
                tower.transform.position = PlaceTrm[idx].position;
                idx++;
            }
        }
    }

    public void CancelMoveTower()
    {
        int idx = 0;
        foreach (TowerBase ownTower in _ownTowerBase)
        {
            ownTower.transform.position = PlaceTrm[idx++].position;
        }
    }

    public void ClearTower()
    {
        _ownTowerBase.Clear();
        CanBuild = true;
    }
}