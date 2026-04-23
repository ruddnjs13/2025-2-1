using System.Collections;
using System.Collections.Generic;
using Code.Combat;
using Code.Core.GameEvent;
using Code.Managers;
using RuddnjsPool;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageable, IPoolable
    {
        [Header("Events")]
        public UnityEvent<int, Enemy> OnHitEvent;
        public UnityEvent OnDeadEvent;

        [Header("References")]
        [SerializeField] protected EnemyRenderer renderer;
        [SerializeField] protected PoolManagerSO poolManager;
        [SerializeField] protected GameEventChannelSO goldChannel;
        [SerializeField] protected GameEventChannelSO systemChannel;
        
        [field: SerializeField] public EnemyMovement movement { get; private set; }
        [field: SerializeField] public EnemyDataSO enemyData { get; private set; }

        [Header("Stats")]
        [field: SerializeField] public int Health { get; protected set; }
        [field: SerializeField] public int Damage { get; private set; }

        public bool IsDead { get; protected set; }
        private Coroutine _deadCoroutine;

        protected static readonly int DeadHash = Animator.StringToHash("DEAD");
        protected static readonly int MoveHash = Animator.StringToHash("MOVE");
        protected const int EnemyLayer = 7;
        protected const int IgnoreLayer = 10;
        private const float DeadDelay = 3f;

        #region Initialization & Core Logic
        public void ResetEnemy(List<Transform> wayPoints)
        {
            StopDeadCoroutine();
            IsDead = false;
            gameObject.layer = EnemyLayer;

            InitEnemy(enemyData.maxHealth, enemyData.damage, enemyData.moveSpeed);
            
            movement.SetStop(false);
            movement.SetWayPoints(wayPoints);
            movement.EnableEnemy();
        }

        public void InitEnemy(int maxHealth, int damage, float moveSpeed)
        {
            Health = maxHealth;
            Damage = damage;
            movement.SetSpeed(moveSpeed);
        }

        public virtual void TakeDamage(int damage)
        {
            if (IsDead) return;

            Health = Mathf.Max(Health - damage, 0);
            renderer.Hit();
            OnHitEvent?.Invoke(Health, this);

            if (Health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            IsDead = true;
            OnDeadEvent?.Invoke();
            
            if (goldChannel != null)
                goldChannel.RaiseEvent(GoldEvent.getGoldEvent.Initialize(enemyData.getGold));

            _deadCoroutine = StartCoroutine(DeadCoroutine());
        }

        public void Arrive()
        {
            if (IsDead) return;
            
            IsDead = true;
            systemChannel.RaiseEvent(SystemEvent.LifeDownEvent);
            ReturnToPool();
        }

        protected virtual IEnumerator DeadCoroutine()
        {
            gameObject.layer = IgnoreLayer;
            movement.SetStop(true);
            renderer.SetParam(DeadHash);

            yield return new WaitForSeconds(DeadDelay);
            
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            WaveManager.Instance.UnregisterEnemy(this);
            movement.resetPosition();
            poolManager.Push(this);
        }

        private void StopDeadCoroutine()
        {
            if (_deadCoroutine != null)
            {
                StopCoroutine(_deadCoroutine);
                _deadCoroutine = null;
            }
        }
        #endregion

        #region Pool Interface
        public float GetDistance() => movement.GetDistance();
        public int GetWaypointIdx() => movement.GetWaypointIdx();

        [field: SerializeField] public PoolTypeSO PoolType { get; set; }
        public GameObject GameObject => gameObject;

        public void SetUpPool(Pool pool) { }

        public virtual void ResetItem()
        {
        }
        #endregion
    }
}