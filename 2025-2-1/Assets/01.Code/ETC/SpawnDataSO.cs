using RuddnjsPool;
using UnityEngine;

namespace _01.Code.ETC
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "SO/SpawnData", order = 0)]
    public class SpawnDataSO : ScriptableObject
    {
        public PoolTypeSO enemyType;
        public int spawnCount;
        public float spawnInterval;
    }
}