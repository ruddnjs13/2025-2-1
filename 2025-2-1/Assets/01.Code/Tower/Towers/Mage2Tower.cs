using System.Collections;
using System.Collections.Generic;
using _01.Code.Enemies;
using Unity.Cinemachine;
using UnityEngine;

namespace _01.Code.Tower.Towers
{
    public class Mage2Tower : TowerBase
    {
        [SerializeField] private ParticleSystem explosionEffect;
        [SerializeField] private SpriteRenderer firePreview;
        [SerializeField] private LayerMask whatIsEnemy;

        [SerializeField] private float explosionRadius = 2f;
        public override void Attack(Enemy target)
        {
            StartCoroutine(ExplosionCoroutine(target));
        }

        private IEnumerator ExplosionCoroutine(Enemy target)
        {
            firePreview.gameObject.SetActive(true);
            firePreview.transform.position = target.transform.position;
            for (int i = 0; i < 3; i++)
            {
                firePreview.color = new Color(1f, 1f, 1f, 0.2f);
                yield return new WaitForSeconds(0.2f);
                firePreview.color = new Color(1f, 1f, 1f, 1f);
                yield return new WaitForSeconds(0.2f);
            }
            explosionEffect.transform.position = firePreview.transform.position;
            explosionEffect.Play();
            
            Collider[] hits = Physics.OverlapSphere(explosionEffect.transform.position, explosionRadius, whatIsEnemy);
            firePreview.gameObject.SetActive(false);

            foreach (Collider hit in hits)
            {
                if(hit.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}