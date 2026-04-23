using System;
using Code.Core.GameEvent;
using Code.Managers;
using Code.Towers;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class UpgradeBtn : MonoBehaviour
    {
        [SerializeField] private towerDataSO data;
        [SerializeField] private GameEventChannelSO goldChannel;
        [SerializeField] private TextMeshProUGUI priceText;

        public int upgradeIdx = 0;

        private void OnEnable()
        {
            priceText.text = data.priceList[upgradeIdx].ToString();
        }

        public void TryUpgrade()
        {
            if (GoldManager.Instance.CheckEnoughGold(data.priceList[upgradeIdx]))
            {
                goldChannel.RaiseEvent(GoldEvent.spendGolEvent.Initialize(data.priceList[upgradeIdx]));
                

                data.damage = data.damageList[upgradeIdx+1];
                upgradeIdx++;

                if (upgradeIdx > data.maxUpgradeCount - 1)
                {
                    gameObject.SetActive(false);
                    return;
                }
                priceText.text = data.priceList[upgradeIdx].ToString();
            }
        }
    }
}