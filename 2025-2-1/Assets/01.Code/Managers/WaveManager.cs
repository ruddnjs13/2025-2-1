using System.Collections;
using System.Collections.Generic;
using Code.Core;
using Code.Enemies;
using Code.ETC;
using RuddnjsPool;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Managers
{
    public class WaveManager : MonoSingleton<WaveManager>
    {
        public UnityEvent<int> OnWaveStart;
        public UnityEvent OnWaveEnd;
        
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private Transform spawnTrm;
        [field: SerializeField] public List<Transform> WayPoints { get; private set; }
        [SerializeField] private List<WaveDataSO> waveInfos = new List<WaveDataSO>();

        private readonly List<Enemy> _enemies = new List<Enemy>();
        private WaitForSeconds _checkInterval;
        private WaitForSeconds _initialWaveDelay;

        protected void Awake()
        {
            _checkInterval = new WaitForSeconds(0.2f);
            _initialWaveDelay = new WaitForSeconds(4f);
        }

        private void Start()
        {
            StartCoroutine(WaveRoutine());
        }

        private IEnumerator WaveRoutine()
        {
            for (int i = 0; i < waveInfos.Count; i++)
            {
                OnWaveStart?.Invoke(i + 1);
                yield return _initialWaveDelay;

                WaveDataSO waveData = waveInfos[i];
                
                foreach (SpawnDataSO group in waveData.spawnGroupList)
                {
                    yield return StartCoroutine(SpawnEnemyRoutine(group));
                }

                yield return new WaitForSeconds(waveData.nextWaveDelay);
            }

            while (_enemies.Count > 0)
            {
                yield return _checkInterval;
            }

            OnWaveEnd?.Invoke();
        }

        private IEnumerator SpawnEnemyRoutine(SpawnDataSO spawnData)
        {
            WaitForSeconds interval = new WaitForSeconds(spawnData.spawnInterval);

            for (int i = 0; i < spawnData.spawnCount; i++)
            {
                if (poolManager.Pop(spawnData.enemyType) is Enemy enemy)
                {
                    enemy.transform.position = spawnTrm.position;
                    enemy.ResetEnemy(WayPoints);
                    RegisterEnemy(enemy);
                }
                yield return interval;
            }
        }

        public void RegisterEnemy(Enemy enemy)
        {
            if (!_enemies.Contains(enemy))
                _enemies.Add(enemy);
        }

        public void UnregisterEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
        }

        public List<Enemy> GetAllEnemies() => _enemies;
    }
}