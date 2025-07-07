using System.Collections;
using Core.GameEvent;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Code.Enemies
{
    public class EnemySkeleton : Enemy
    {
        public UnityEvent OnReviveEvent;
        private int life = 1;
        
        protected override IEnumerator DeadCoroutine()
        {
            if (life <= 0)
            {
                base.DeadCoroutine();
                yield break;
            }
            
            movement.SetStop(true);
            renderer.SetParam(_deadHash);
            IsDead = true;
            yield return new WaitForSeconds(3f);
            OnReviveEvent?.Invoke();
            movement.SetStop(false);
            renderer.SetParam(_moveHash);
            IsDead = false; 
            life -= 1;
            Health = enemyData.maxHealth;
        }

        public override void TakeDamage(int damage)
        {
            if (IsDead) return;
            renderer.Hit();
            Health = Mathf.Clamp(Health - damage,0, enemyData.maxHealth);
            OnHitEvent?.Invoke(Health,this);
            if (Health <= 0)
            {
                if (life > 0)
                {
                    OnDeadEvent?.Invoke();
                    StartCoroutine(DeadCoroutine());
                }
                else
                {
                    goldChannel.RaiseEvent(GoldEvent.getGoldEvent.Initialize(enemyData.getGold));
                    IsDead = true;
                    OnDeadEvent?.Invoke();
                    StartCoroutine(base.DeadCoroutine());
                }
            }
        }

        public override void ResetItem()
        {
            base.ResetItem();
            life = 1;
        }
    }
}