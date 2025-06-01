using _01.Code.Tower.Towers;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlaceTile : MonoBehaviour
{
    [field: SerializeField] public Transform PlaceTrm { get; set; }
    
    public bool CanBuild { get; private set; } = true;
    
    private TowerBase _ownTowerBase;

    public void SetTower(TowerBase towerBase)
    {
        if(!CanBuild) return;
        
        _ownTowerBase = towerBase;
        CanBuild = false;
    }
}