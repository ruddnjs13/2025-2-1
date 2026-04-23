using Code.Core.GameEvent;
using Code.Managers;
using DG.Tweening;
using UnityEngine;

namespace Code.UI
{
    public class LifeUI : MonoBehaviour
    {
        [SerializeField] private Transform trm;
        [SerializeField] private GameObject heartPrefab;

        [SerializeField] private GameEventChannelSO systemChannel;

        private void Start()
        {
            systemChannel.AddListener<LifeDownEvent>(HandleLifeDown);
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        private void HandleLifeDown(LifeDownEvent evt)
        {
            DOVirtual.DelayedCall(0.2f,() =>
            {
                foreach (Transform child in trm)
                {
                    Destroy(child.gameObject);
                }
                for (int i = 0; i < GameManager.Instance.HealthPoint; i++)
                {
                    Instantiate(heartPrefab, trm);
                }
            });
            
        }
    }
}