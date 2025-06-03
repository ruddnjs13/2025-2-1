using System.Collections.Generic;
using RuddnjsPool;
using UnityEngine;

namespace _01.Code.ETC
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "SO/WaveData", order = 0)]
    public class WaveDataSO : ScriptableObject
    {
        public float nextWaveDelay;
        [SerializeField] public List<SpawnDataSO> spawnGroupList = new List<SpawnDataSO>();
        
    }
}