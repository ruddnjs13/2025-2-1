using UnityEngine;

namespace _01.Code.Enemies
{
    public class EnemyRenderer : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Animator _aniamtor;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _aniamtor = GetComponent<Animator>();
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
        
        public void SetParam(int hashValue,bool value) => _aniamtor.SetBool(hashValue, value);
        public void SetParam(int hashValue,int value) => _aniamtor.SetInteger(hashValue, value);
        public void SetParam(int hashValue,float value) => _aniamtor.SetFloat(hashValue, value);
        public void SetParam(int hashValue) => _aniamtor.SetTrigger(hashValue);
    }
}