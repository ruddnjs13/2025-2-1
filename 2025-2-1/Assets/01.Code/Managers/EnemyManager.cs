using System;
using System.Collections.Generic;
using _01.Code.Enemies;
using UnityEngine;

namespace _01.Code.Managers
{
    public class EnemyManager : MonoSingleton<EnemyManager>
    {

        [SerializeField] private List<Enemy> enemies = new List<Enemy>();

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