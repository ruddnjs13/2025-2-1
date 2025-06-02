using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using _01.Code.Enemies;
using RuddnjsPool;
using UnityEngine;

namespace _01.Code.Managers
{
    public class EnemyManager : MonoSingleton<EnemyManager>
    {

        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private PoolTypeSO enemyPoolType;
        [SerializeField] private List<Enemy> enemies = new List<Enemy>();
        [SerializeField] private Transform spawnTrm;
        [SerializeField] private List<Transform> way = new List<Transform>();
        private void Start()
        {
            StartCoroutine(waveCoroutine());
        }

        private IEnumerator waveCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(3);
                Enemy enemy = poolManager.Pop(enemyPoolType) as Enemy;
                enemy.ResetEnemy(way);
                enemy.transform.position = spawnTrm.position;
                RegisterEnemy(enemy);

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