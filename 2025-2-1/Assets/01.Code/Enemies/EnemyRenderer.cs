using System.Collections;
using UnityEngine;

namespace Code.Enemies
{
    public class EnemyRenderer : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float blinkTime = 0.2f;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Material _targetMat;
        private Camera _mainCamera;

        private WaitForSeconds _blinkWait;
        private Coroutine _blinkCoroutine;
        private static readonly int IsHitValue = Shader.PropertyToID("_IsHit");

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _targetMat = _spriteRenderer.material;
            
            _mainCamera = Camera.main;
            _blinkWait = new WaitForSeconds(blinkTime);
        }

        private void OnDisable()
        {
            ResetHitEffect();
        }

        public void FlipByCamera(Vector3 moveDirection)
        {
            bool isFlip = Vector3.Dot(moveDirection, _mainCamera.transform.right) > 0;
            _spriteRenderer.flipX = isFlip;
        }

        public void Hit()
        {
            if (_blinkCoroutine != null)
            {
                StopCoroutine(_blinkCoroutine);
            }
            _blinkCoroutine = StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            _targetMat.SetInt(IsHitValue, 1);
            yield return _blinkWait;
            _targetMat.SetInt(IsHitValue, 0);
            _blinkCoroutine = null;
        }

        private void ResetHitEffect()
        {
            if (_blinkCoroutine != null)
            {
                StopCoroutine(_blinkCoroutine);
                _blinkCoroutine = null;
            }
            _targetMat.SetInt(IsHitValue, 0);
        }

        #region Animator Parameters
        public void SetParam(int hashValue, bool value) => _animator.SetBool(hashValue, value);
        public void SetParam(int hashValue, int value) => _animator.SetInteger(hashValue, value);
        public void SetParam(int hashValue, float value) => _animator.SetFloat(hashValue, value);
        public void SetParam(int hashValue) => _animator.SetTrigger(hashValue);
        #endregion
    }
}