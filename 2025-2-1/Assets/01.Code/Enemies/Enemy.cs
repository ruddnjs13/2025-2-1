using System.Collections;
using System.Collections.Generic;
using _01.Code.Combat;
using _01.Code.Managers;
using Core.GameEvent;
using RuddnjsPool;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Code.Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageable, IPoolable
    {
        public UnityEvent OnHitEvent;
        public UnityEvent OnDeadEvent;
        
        [SerializeField] private EnemyRenderer renderer;
        [SerializeField] private EnemyMovement movement;
        [SerializeField] private EnemyDataSO enemyData;
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private GameEventChannelSO goldChannel;
        
        [Header("EnemyStat")]
        [field:SerializeField] public int Health { get; private set; }
        [field:SerializeField] public int Damage { get; private set; }

        private readonly int _deadHash = Animator.StringToHash("DEAD");

        public bool IsDead { get; private set; } = false;
        
        public void ResetEnemy(List<Transform> wayPoints)
        {
            movement.SetWayPoints(wayPoints);
            InitEnemy(enemyData.maxHealth, enemyData.damage, enemyData.moveSpeed);
        }

        public void InitEnemy(int maxHealth, int damage, float moveSpeed)
        {
            Health = maxHealth;
            Damage = damage;
            movement.SetSpeed(moveSpeed);
            movement.EnableEnemy();
        }

        public void TakeDamage(int damage)
        {
            Health = Mathf.Clamp(Health - damage,0, enemyData.maxHealth);
            OnHitEvent?.Invoke();
            if (Health <= 0 && !IsDead)
            {
                IsDead = true;
                OnDeadEvent?.Invoke();
                goldChannel.RaiseEvent(GoldEvent.getGoldEvent.Initialize(enemyData.getGold));
                StartCoroutine(DeadCoroutine());
            }
        }

        private IEnumerator DeadCoroutine()
        {
            movement.SetStop(true);
            renderer.SetParam(_deadHash);
            yield return new WaitForSeconds(3f);
            EnemyManager.Instance.UnregisterEnemy(this);
            poolManager.Push(this);
        }

        #region Pool
        public float GetDistance() => movement.GetDistance();
        public int GetWaypointIdx() => movement.GetWaypointIdx();
        [field:SerializeField] public PoolTypeSO PoolType { get; set; }
        public GameObject GameObject => gameObject;

        private Pool _myPool;
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
            IsDead = false;
            movement.SetStop(false);
        }
        #endregion
    }
}