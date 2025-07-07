using System;
using System.Collections;
using System.Collections.Generic;
using _01.Code.Enemies;
using _01.Code.ETC;
using _01.Code.Managers;
using RuddnjsPool;
using Settings.InputSettings;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace _01.Code.Tower.Towers
{
    public enum TowerType
    {
        ARCHER = 0,MAGE = 1, KNIGHT = 2, MAGE2 =3
    }
    
    public abstract class TowerBase : MonoBehaviour, ITargeting, IAttack
    {
        public UnityEvent OnAttackEvent;
        
        public TowerType towerType;
        
        public float range = 5f;
        public float fireRate = 2f;
        [SerializeField] protected towerDataSO towerData;
        [SerializeField] protected Transform firePos;
        [SerializeField] protected PoolTypeSO projectileType;
        [SerializeField] protected PoolManagerSO poolManager;
        [SerializeField] protected Animator animator;
        [SerializeField] protected InputReaderSO inputReader;
        
        private readonly int dirXHash = Animator.StringToHash("DirX");
        private readonly int dirYHash = Animator.StringToHash("DirY");

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
        
        #region AttackLogic
        public void EnableTower()
        {
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator  AttackCoroutine()
        {
            WaitForSeconds wait = new WaitForSeconds(fireRate);
            
            yield return new WaitForSeconds(fireRate/2);
            while (true)
            {
                Enemy target = FindTarget(WaveManager.Instance.GetAllEnemies());
                if (target != null && target.IsDead == false)
                {
                    Vector3 direction = (target.transform.position - transform.position).normalized;
                    direction = Quaternion.Euler(0,-45,0) * direction;

                    animator.SetFloat(dirXHash, direction.x);
                    animator.SetFloat(dirYHash, direction.z);

                    animator.SetTrigger("ATTACK");
                    OnAttackEvent?.Invoke();
                    Attack(target);
                    yield return wait;
                }
                yield return null;
            }
        }
        
        
        public virtual Enemy FindTarget( List<Enemy> enemies)
        {
            Enemy target = null;
            
            foreach (Enemy enemy in enemies)
            {
                if (enemy == null || enemy.IsDead) continue; // null 체크

                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist <= range)
                {
                    if (target == null) target = enemy;

                    if (enemy.GetWaypointIdx() > target.GetWaypointIdx())
                    {
                        target = enemy;
                    }
                    else if (enemy.GetWaypointIdx() == target.GetWaypointIdx())
                    {
                        if (enemy.GetDistance() < target.GetDistance())
                        {
                            target = enemy;
                        }
                    }
                }
            }

            return target;
        }

        public virtual void Attack(Enemy target) {}
        
        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);
        }
#endif
        #endregion

        public void StartDrag()
        {
            StopAllCoroutines();
            StartCoroutine(DragCoroutine());
        }

        public void EndDrag()
        {
            StopAllCoroutines();
            StartCoroutine(AttackCoroutine());
            
        }

        private IEnumerator DragCoroutine()
        {
            while (true)
            {
                Vector3 screenPos = new Vector3(inputReader.MousePosition.x, inputReader.MousePosition.y, -Camera.main.transform.position.z);

                transform.position = Camera.main.ScreenToWorldPoint(screenPos);
                yield return null;
            }
        }
    }
}
