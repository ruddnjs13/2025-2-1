using System;
using _01.Code.Enemies;
using Unity.VisualScripting;
using UnityEngine;

namespace _01.Code.Tower.Towers
{
    public class KnightTower : TowerBase
    {
        [SerializeField] private LayerMask whatIsEnemy;
        [SerializeField] private ParticleSystem slashEffect;
        public override void Attack(Enemy target)
        {
            firePos.transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
            slashEffect.Play();
            Collider[] hits = Physics.OverlapSphere(slashEffect.transform.position, range, whatIsEnemy);

            foreach (Collider hit in hits)
            {
                if(hit.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(towerData.damage);
                }
            }
        }

#if UNITY_EDITOR
        // private void OnDrawGizmos()
        // {
        //     Gizmos.DrawWireSphere(slashEffect.transform.position, range);
        // }
#endif
    }
}