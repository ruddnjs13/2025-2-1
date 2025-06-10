using _01.Code.Enemies;
using Feedbacks.VFX;
using RuddnjsPool;
using UnityEngine;

namespace _01.Code.Combat.Projectiles
{
    public class ExplosionBall : Projectile
    {
        [SerializeField] private float explosionRadius = 2f;
        [SerializeField] private PoolTypeSO effectType;
        public override void OnTriggerEnter(Collider collision)
        {
            VFXPlayer effect = poolManager.Pop(effectType) as VFXPlayer;
            effect.SetUpAndPlay(transform.position);
            if (((1 << collision.gameObject.layer) & whatIsEnemy) != 0)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, whatIsEnemy);

                foreach (Collider hit in hits)
                {
                    if(hit.TryGetComponent(out Enemy enemy))
                    {
                        enemy.TakeDamage(damage);
                    }
                }
                poolManager.Push(this);
            }
        }
    }
}