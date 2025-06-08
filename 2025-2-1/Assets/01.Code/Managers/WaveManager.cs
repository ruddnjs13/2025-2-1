using System.Collections;
using System.Collections.Generic;
using _01.Code.Enemies;
using _01.Code.ETC;
using RuddnjsPool;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Code.Managers
{
    public class WaveManager : MonoSingleton<WaveManager>
    {
        public UnityEvent<int> OnWaveStart;
        public UnityEvent OnWaveEnd;
        
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private List<Enemy> enemies = new List<Enemy>();
        [SerializeField] private Transform spawnTrm;
        [SerializeField] private List<Transform> way = new List<Transform>();
        [SerializeField] private List<WaveDataSO> waveInfos = new List<WaveDataSO>();
        private void Start()
        {
            StartCoroutine(WaveCoroutine());
        }

        private IEnumerator WaveCoroutine()
        {
            for (int i = 0; i < waveInfos.Count; i++)
            {
                OnWaveStart?.Invoke(i);
                for (int j = 0; j < waveInfos[i].spawnGroupList.Count; j++)
                {
                    WaitForSeconds wait = new WaitForSeconds(waveInfos[i].spawnGroupList[j].spawnInterval * 
                                                             waveInfos[i].spawnGroupList[j].spawnCount);
                    StartCoroutine(SpawnEnemy(waveInfos[i].spawnGroupList[j]));
                    yield return wait;
                }
                yield return new WaitForSeconds(waveInfos[i].nextWaveDelay);
            }

            while (true)
            {
                yield return new WaitForSeconds(0.2f);
                if (enemies.Count <= 0)
                {
                    yield return new WaitForSeconds(2);
                    OnWaveEnd?.Invoke();
                    break;
                }
            }
        }

        private IEnumerator SpawnEnemy(SpawnDataSO spawnData)
        {
            for (int i = 0; i < spawnData.spawnCount; i++)
            {
                Enemy enemy = poolManager.Pop(spawnData.enemyType) as Enemy;
                enemy.ResetEnemy(way);
                enemy.transform.position = spawnTrm.position;
                RegisterEnemy(enemy);
                yield return new WaitForSeconds(spawnData.spawnInterval);
            }
        }
        
        

        public void RegisterEnemy(Enemy enemy)
        {
            if (!enemies.Contains(enemy))
                enemies.Add(enemy);
        }

        public void UnregisterEnemy(Enemy enemy)
        {
            if (enemies.Contains(enemy))
                enemies.Remove(enemy);
        }

        public List<Enemy> GetAllEnemies()
        {
            return enemies;
        }
    }
}