using Code.Enemies;
using UnityEngine;

namespace Code.Towers
{
    public class KnightTower : TowerBase
    {
        [SerializeField] private LayerMask whatIsEnemy;
        [SerializeField] private ParticleSystem slashEffect;
        [SerializeField] private float attackRadius;
        
        public override void Attack(Enemy target)
        {
            firePos.transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
            slashEffect.Play();
            Collider[] hits = Physics.OverlapSphere(slashEffect.transform.position, attackRadius, whatIsEnemy);

            foreach (Collider hit in hits)
            {
                if(hit.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(towerData.damage);
                }
            }
        }
    }
}