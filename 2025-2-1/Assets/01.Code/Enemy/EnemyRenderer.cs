using System;
using UnityEngine;

namespace _01.Code.Enemy
{
    public class EnemyRenderer : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void FlipByCamera(Vector3 moveDirection)
        {
            bool isFlip = Vector3.Dot(moveDirection,Camera.main.transform.right) > 0;
            
            _spriteRenderer.flipX = isFlip;
        }
    }
}