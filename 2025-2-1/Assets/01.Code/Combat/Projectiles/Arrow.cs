using UnityEngine;

namespace _01.Code.Combat.Projectiles
{
    public class Arrow : Projectile
    {
        protected override void MoveTowardsTarget()
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
            transform.right = -dir; 
        }
    }
}