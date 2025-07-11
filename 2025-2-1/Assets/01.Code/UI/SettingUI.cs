using System;
using DG.Tweening;
using UnityEngine;

namespace _01.Code.UI
{
    public class SettingUI : MonoBehaviour
    {
        [SerializeField] private float popUpTime = 1.4f;
        [SerializeField] private int popUpAmount = 1400;
        
        private RectTransform settingUI;
        private Vector2 originPos;

        private bool isMove = false;

        private void OnDestroy()
        {
            Time.timeScale = 1;
            DOTween.Kill(settingUI);
        }

        private void Start()
        {
            settingUI = transform as RectTransform;
            originPos = settingUI.anchoredPosition;
        }

        public void Setup(bool isActive)
        {
            if (isMove || settingUI == null) return;
            isMove = true;
            settingUI.DOAnchorPosY(originPos.y + (isActive ? -popUpAmount : popUpAmount), popUpTime)
                .SetEase(Ease.OutBounce)
                .OnComplete(() =>
                {
                    Time.timeScale = isActive ? 1 : 0;
                    isMove = false;
                }).SetUpdate(true);
            
        }
    }
}