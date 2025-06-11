using System.Collections;
using UnityEngine;

namespace _01.Code.Enemies
{
    public class EnemySkeleton : Enemy
    {
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
            renderer.SetParam(_moveHash);
            IsDead = false; life -= 1;
            Health = enemyData.maxHealth;
        }

        public override void ResetItem()
        {
            base.ResetItem();
            life = 1;
        }
    }
}