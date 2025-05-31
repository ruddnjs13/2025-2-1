using UnityEngine;

namespace RuddnjsPool.RuddnjsLib.Pool.RunTime
{
    public class PoolManagerMono : MonoBehaviour
    {
        [SerializeField] private PoolManagerSO _poolManager;

        private void Awake()
        {
            _poolManager.InitializePool(transform);
        }
    }
}
