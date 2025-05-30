using System;
using _01.Code.Combat;
using _01.Code.ETC;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Code.Enemy
{
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        public UnityEvent OnHitEvent;
        public UnityEvent OnDeadEvent;
        
        [SerializeField] private EnemyRenderer renderer;
        [SerializeField] private EnemyMovement movement;
        [SerializeField] private EnemyDataSO enemyData;
        
        [Header("EnemyStat")]
        [field:SerializeField] public int Health { get; private set; }
        [field:SerializeField] public int Damage { get; private set; }

        private void Start()
        {
            ResetEnemy();
        }

        private void ResetEnemy()
        {
            InitEnemy(enemyData.maxHealth, enemyData.damage, enemyData.moveSpeed);
        }

        public void InitEnemy(int maxHealth, int damage, int moveSpeed)
        {
            Health = maxHealth;
            Damage = damage;
            movement.SetSpeed(moveSpeed);
        }

        public void TakeDamage(int damage)
        {
            Health = Mathf.Clamp(0, enemyData.maxHealth,Health - damage);
            OnHitEvent?.Invoke();
            if (Health <= 0)
            {
                OnDeadEvent?.Invoke();
            }
        }
    }
}