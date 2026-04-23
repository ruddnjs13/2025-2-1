using System;
using Code.Towers;
using Settings.InputSettings;
using UnityEngine;

namespace Code.ETC
{
    public class ResetData : MonoBehaviour
    {
        [SerializeField] private towerDataSO[] towerData;
        [SerializeField] private InputReaderSO inputReader;

        private void Start()
        {
            Reset();
        }

        public void Reset()
        {
            inputReader.RockInput(true);
            foreach (towerDataSO data in towerData)
            {
                data.ResetData();
            }
        }
    }
}