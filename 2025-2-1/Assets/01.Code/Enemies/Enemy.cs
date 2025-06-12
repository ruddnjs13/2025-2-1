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
        public UnityEvent<int, Enemy> OnHitEvent;
        public UnityEvent OnDeadEvent;
        
        [SerializeField] protected EnemyRenderer renderer;
        [field:SerializeField] public EnemyMovement movement { get; private set; }
        [SerializeField] protected PoolManagerSO poolManager;
        [SerializeField] protected GameEventChannelSO goldChannel;
        [SerializeField] protected GameEventChannelSO systemChannel;
        [field:SerializeField] public EnemyDataSO enemyData { get; private set; }
        
        [Header("EnemyStat")]
        [field:SerializeField] public int Health { get; protected set; }
        [field:SerializeField] public int Damage { get; private set; }

        protected readonly int _deadHash = Animator.StringToHash("DEAD");
        protected readonly int _moveHash = Animator.StringToHash("MOVE");

        public bool IsDead { get; protected set; } = false;
        
        protected readonly int enemyLayer = 7;
        protected readonly int ignoreLayer = 10;
        
        public void ResetEnemy(List<Transform> wayPoints)
        {            
            movement.SetStop(false);
            InitEnemy(enemyData.maxHealth, enemyData.damage, enemyData.moveSpeed);
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
            renderer.Hit();
            Health = Mathf.Clamp(Health - damage,0, enemyData.maxHealth);
            OnHitEvent?.Invoke(Health,this);
            if (Health <= 0)
            {
                IsDead = true;
                OnDeadEvent?.Invoke();
                goldChannel.RaiseEvent(GoldEvent.getGoldEvent.Initialize(enemyData.getGold));
                StartCoroutine(DeadCoroutine());
            }
        }

        public void Arrive()
        {
            WaveManager.Instance.UnregisterEnemy(this);
            systemChannel.RaiseEvent(SystemEvent.LifeDownEvent);
            IsDead = true;
            movement.SetStop(true);
            movement.resetPosition();
            poolManager.Push(this);
        }

        protected virtual IEnumerator DeadCoroutine()
        {
            gameObject.layer = ignoreLayer;
            movement.SetStop(true);
            renderer.SetParam(_deadHash);
            yield return new WaitForSeconds(3f);
            WaveManager.Instance.UnregisterEnemy(this);
            movement.resetPosition();
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

        public virtual void ResetItem()
        {
            IsDead = false;
            gameObject.layer = enemyLayer;
        }
        #endregion
    }
}