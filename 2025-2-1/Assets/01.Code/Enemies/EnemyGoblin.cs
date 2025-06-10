using Unity.VisualScripting;
using UnityEngine;

namespace _01.Code.Enemies
{
    public class EnemyGoblin : Enemy
    {
        private readonly int _runHash = Animator.StringToHash("RUN");
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if (Health <= enemyData.maxHealth / 2)
            {
                movement.SetSpeed(enemyData.moveSpeed * 1.5f);
                renderer.SetParam(_runHash);
            }
        }
    }
}