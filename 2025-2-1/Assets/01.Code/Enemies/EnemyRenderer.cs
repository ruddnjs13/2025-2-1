using System;
using System.Collections;
using UnityEngine;

namespace _01.Code.Enemies
{
    public class EnemyRenderer : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Animator _aniamtor;
        
        private readonly int _isHitValue = Shader.PropertyToID("_IsHit");

        private Material _targetMat;

        [SerializeField] private float blinkTime = 0.2f;
        

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _aniamtor = GetComponent<Animator>();
            
            _targetMat = _spriteRenderer.material;
        }
        
        public void FlipByCamera(Vector3 moveDirection)
        {
            bool isFlip = Vector3.Dot(moveDirection,Camera.main.transform.right) > 0;
            
            _spriteRenderer.flipX = isFlip;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            _targetMat.SetInt(_isHitValue, 0);
        }

        public void Hit()
        {
            _targetMat.SetInt(_isHitValue, 0);
            StopAllCoroutines();
            _targetMat.SetInt(_isHitValue, 1);
            StartCoroutine(DelayCoroutine());
        }

        private IEnumerator DelayCoroutine()
        {
            yield return new WaitForSeconds(blinkTime);
            _targetMat.SetInt(_isHitValue, 0);
        }

        public void SetParam(int hashValue,bool value) => _aniamtor.SetBool(hashValue, value);
        public void SetParam(int hashValue,int value) => _aniamtor.SetInteger(hashValue, value);
        public void SetParam(int hashValue,float value) => _aniamtor.SetFloat(hashValue, value);
        public void SetParam(int hashValue) => _aniamtor.SetTrigger(hashValue);
    }
}