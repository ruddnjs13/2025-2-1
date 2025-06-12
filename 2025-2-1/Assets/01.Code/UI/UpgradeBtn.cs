using System;
using _01.Code.Managers;
using _01.Code.Tower;
using Core.GameEvent;
using TMPro;
using UnityEngine;

namespace _01.Code.UI
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
                priceText.text = data.priceList[upgradeIdx+1].ToString();
            }
        }
    }
}