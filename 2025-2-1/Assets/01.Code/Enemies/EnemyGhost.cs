using System;
using System.Collections;
using UnityEngine;

namespace _01.Code.Enemies
{
    public class EnemyGhost : Enemy
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float ignoreTime = 2f;
        private Color originColor = new Color(1,1,1,1);
        private Color transparentColor = new Color(1, 1, 1, 0.4f);

        private bool canUseSkill = true;

        private WaitForSeconds wait;

        


        private void Start()
        {
            wait = new WaitForSeconds(ignoreTime);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }


        public override void TakeDamage(int damage)
        {
            if (Health <= enemyData.maxHealth / 2 && canUseSkill)
            {
                canUseSkill = false;
                StartCoroutine(IgnoreCoroutine());
            }
            base.TakeDamage(damage);
        }

        private IEnumerator IgnoreCoroutine()
        {
            gameObject.layer = ignoreLayer;
            spriteRenderer.color = transparentColor;
            IsDead = true;
            yield return wait;
            IsDead = false;
            gameObject.layer = enemyLayer;
            spriteRenderer.color = originColor;
        }

        public override void ResetItem()
        {
            base.ResetItem();
            canUseSkill = true;
        }
    }
}