using System;
using Core.GameEvent;
using UnityEngine;

namespace _01.Code.Managers
{
    public class GoldManager : MonoSingleton<GoldManager>
    {
        public int Gold { get; private set; }

        [SerializeField] private GameEventChannelSO goldChannel;

        public bool CheckEnoughGold(int useAmount)
        {
            return Gold >= useAmount;
        }

        private void Start()
        {
            goldChannel.AddListener<GetGoldEvent>(HandleGetGold);
            goldChannel.AddListener<SpendGoldEvent>(HandleSpendGold);
        }

        private void HandleSpendGold(SpendGoldEvent evt)
        {
            Gold -= evt.spendAmount;
        }

        private void HandleGetGold(GetGoldEvent evt)
        {
            Gold += evt.getAmount;
        }
    }
}