using System;
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
        ARCHER = 0,
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

        private Enemy _target;

        public bool EnableTower { get; set; } = false;
    
        private float fireTimer;


        protected virtual void Update()
        {
            if(!EnableTower) return;
            CalculateTimer();
        }

        private void CalculateTimer()
        {
            if (fireTimer <= 0f)
            {
                Enemy target = FindTarget(EnemyManager.Instance.GetAllEnemies());
                if (target != null && target.IsDead == false)
                {
                    Vector3 direction = (target.transform.position - transform.position).normalized;
                    direction = Quaternion.Euler(0,-45,0) * direction;
                    
                    animator.SetFloat(dirXHash, direction.x);
                    animator.SetFloat(dirYHash, direction.z);
                    
                    fireTimer = fireRate;
                    _target = target;
                    animator.SetTrigger("ATTACK");
                    Debug.Log("발사");
                    Attack(target);
                }
            }

            fireTimer -= Time.deltaTime;
        }

        public virtual Enemy FindTarget(List<Enemy> enemies)
        {
            
            if (enemies.Count <= 0) return null;
        
            Enemy target = enemies[0];

            for (int i = 1; i < enemies.Count; i++)
            {
                if(enemies[i].IsDead) continue;
                if (target.GetWaypointIdx() < enemies[i].GetWaypointIdx())
                {
                    target = enemies[i];
                }
                else if (target.GetWaypointIdx() == enemies[i].GetWaypointIdx())
                {
                    if (enemies[i].GetDistance() < target.GetDistance())
                        target = enemies[i];
                }
            }

            if (target.GetDistance() >= range) return null;
            return target;
        }

        public virtual void Attack(Enemy target)
        {
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);
        }
#endif
    }
    
    
}