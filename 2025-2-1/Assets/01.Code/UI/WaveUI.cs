using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _01.Code.UI
{
    public class WaveUI : MonoBehaviour
    {
        [SerializeField] private RectTransform wavePanel;
        [SerializeField] private TextMeshProUGUI waveText;

        private Vector2 startPosition;

        private void OnEnable()
        {
            startPosition = wavePanel.anchoredPosition;
        }


        public void PopUpWavePanel(int waveIndex)
        {
            if(wavePanel == null) return;
            
            Sequence sequence =  DOTween.Sequence();

            waveText.text = $"Wave {waveIndex + 1}";

            sequence.Append(wavePanel.DOAnchorPosY(startPosition.y - 300, 1f).SetEase(Ease.OutExpo))
                .AppendInterval(2)
                .Append(wavePanel.DOAnchorPosY(startPosition.y + 300, 0.6f).SetEase(Ease.OutExpo))
                .SetUpdate(true);

            sequence.SetLink(gameObject);
        }
    }
}