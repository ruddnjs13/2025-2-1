using UnityEngine;

namespace _01.Code.Tower.Tower
{
    public enum TowerType
    {
        Tower1 = 0,
    }
    
    public abstract class Tower : MonoBehaviour
    {
        public TowerType towerType;
        
        
    }
}