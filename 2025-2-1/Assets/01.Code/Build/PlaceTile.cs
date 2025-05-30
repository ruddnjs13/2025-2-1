using _01.Code.Tower.Tower;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlaceTile : MonoBehaviour
{
    [field: SerializeField] public Transform PlaceTrm { get; set; }
    
    public bool CanBuild { get; private set; } = true;
    
    private Tower ownTower;

    public void SetTower(Tower tower)
    {
        if(!CanBuild) return;
        
        ownTower = tower;
        CanBuild = false;
    }
}