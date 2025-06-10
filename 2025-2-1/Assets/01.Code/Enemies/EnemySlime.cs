using _01.Code.Managers;
using Core.GameEvent;
using RuddnjsPool;
using UnityEngine;

namespace _01.Code.Enemies
{
    public class EnemySlime : Enemy
    {
        [SerializeField] private PoolTypeSO babySlimeType;

        private float[] spawnPoints = { -0.5f, -1 };
        public override void TakeDamage(int damage)
        {
            //base.TakeDamage(damage);
            
            if (IsDead) return;
            renderer.Hit();
            Health = Mathf.Clamp(Health - damage,0, enemyData.maxHealth);
            OnHitEvent?.Invoke(Health,this);

            if (Health <= 0)
            {
                Vector3 spawnCenter = transform.position;
                
                for (int i = 0; i < 2; i++)
                {
                    Enemy babySlime = poolManager.Pop(babySlimeType) as Enemy;
                    babySlime.transform.position = spawnCenter + transform.right * spawnPoints[i];
                    babySlime.ResetEnemy(WaveManager.Instance.WayPoints);
                    babySlime.movement.SetIdx(movement._currentIndex);
                    WaveManager.Instance.RegisterEnemy(babySlime);
                }
                
                IsDead = true;
                OnDeadEvent?.Invoke();
                goldChannel.RaiseEvent(GoldEvent.getGoldEvent.Initialize(enemyData.getGold));
                StartCoroutine(DeadCoroutine());
            }
        }
    }
}