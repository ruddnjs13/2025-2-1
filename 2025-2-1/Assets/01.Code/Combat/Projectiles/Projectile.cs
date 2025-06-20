using _01.Code.Enemies;
using RuddnjsPool;
using UnityEngine;

namespace _01.Code.Combat.Projectiles
{
    public abstract class Projectile : MonoBehaviour , IPoolable
    {
        [SerializeField] protected PoolManagerSO poolManager;
        [SerializeField] protected float speed;
        [SerializeField] protected int damage;
        [SerializeField] protected LayerMask whatIsEnemy;
        protected Enemy target;

        protected bool isFire = false;
        
        protected virtual void Update()
        {
            if (isFire)
            {
                if (target == null || target.IsDead)
                {
                    OnTargetLost();
                }
                MoveTowardsTarget();
            }
        }

        public void SetTargetAndFire(Enemy targetEnemy, int damage)
        {
            this.damage = damage;
            target = targetEnemy;
            isFire = true;
        }

        protected virtual void MoveTowardsTarget()
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
            transform.forward = dir; 
        }

        public virtual void OnTriggerEnter(Collider collision)
        {

            if (((1 << collision.gameObject.layer) & whatIsEnemy) != 0)
            {
                Debug.Log("부딪힘");
                if (collision.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }

                poolManager.Push(this);
            }
        }

        protected virtual void OnTargetLost() => poolManager.Push(this);
        
        #region Pool

        [field:SerializeField] public PoolTypeSO PoolType { get; set; }
        public GameObject GameObject => gameObject;

        private Pool _myPool;
        
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
            isFire = false;
            target = null;
        }

        #endregion
    }
}