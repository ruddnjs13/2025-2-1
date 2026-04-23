using Code.Core;
using Code.Core.GameEvent;
using TMPro;
using UnityEngine;

namespace Code.Managers
{
    public class GoldManager : MonoSingleton<GoldManager>
    {
        [SerializeField] private GameEventChannelSO goldChannel;
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private int startGold = 100;

        public int Gold { get; private set; }

        private void Start()
        {
            goldChannel.AddListener<GetGoldEvent>(HandleGetGold);
            goldChannel.AddListener<SpendGoldEvent>(HandleSpendGold);
            
            UpdateGold(startGold);
        }

        private void OnDestroy()
        {
            if (goldChannel == null) return;
            goldChannel.RemoveListener<GetGoldEvent>(HandleGetGold);
            goldChannel.RemoveListener<SpendGoldEvent>(HandleSpendGold);
        }

        public bool CheckEnoughGold(int useAmount) => Gold >= useAmount;

        private void HandleGetGold(GetGoldEvent evt) => UpdateGold(evt.getAmount);

        private void HandleSpendGold(SpendGoldEvent evt) => UpdateGold(-evt.spendAmount);

        private void UpdateGold(int amount)
        {
            Gold += amount;
            
            if (goldText != null)
                goldText.text = Gold.ToString("N0");
                
            goldChannel.RaiseEvent(GoldEvent.goldValueChangeEvent);
        }
    }
}