using System;
using System.Collections;
using System.Collections.Generic;
using _01.Code.Enemies;
using _01.Code.ETC;
using _01.Code.Managers;
using RuddnjsPool;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Serialization;

namespace _01.Code.Tower.Towers
{
    public enum TowerType
    {
        ARCHER = 0,MAGE = 1
    }
    
    public abstract class TowerBase : MonoBehaviour, ITargeting, IAttack
    {
        public TowerType towerType;
        
        public float range = 5f;
        public float fireRate = 2f;
        public int damage = 2;
        [SerializeField] protected Transform firePos;
        [SerializeField] protected PoolTypeSO projectileType;
        [SerializeField] protected PoolManagerSO poolManager;
        [SerializeField] protected Animator animator;
        [SerializeField] protected AnimationTrigger animationTrigger;
        
        private readonly int dirXHash = Animator.StringToHash("DirX");
        private readonly int dirYHash = Animator.StringToHash("DirY");

        

        public void EnableTower()
        {
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator  AttackCoroutine()
        {
            while (true)
            {
                Enemy target = FindTarget(EnemyManager.Instance.GetAllEnemies());
                if (target != null && target.IsDead == false)
                {
                    Vector3 direction = (target.transform.position - transform.position).normalized;
                    direction = Quaternion.Euler(0,-45,0) * direction;
                    
                    animator.SetFloat(dirXHash, direction.x);
                    animator.SetFloat(dirYHash, direction.z);
                    
                    animator.SetTrigger("ATTACK");
                    Attack(target);
                    yield return new WaitForSeconds(fireRate);
                }

                yield return null;
            }
        }
        
        
        public virtual Enemy FindTarget( List<Enemy> enemies)
        {
            Enemy closest = null;
            float minDistance = float.MaxValue;
            
            foreach (Enemy enemy in EnemyManager.Instance.GetAllEnemies())
            {
                if (enemy == null) continue; // null 체크

                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist <= range && dist < minDistance)
                {
                    closest = enemy;
                    minDistance = dist;
                }
            }

            return closest;
        }

        public virtual void Attack(Enemy target) {}
        
        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);
        }
#endif
    }
}