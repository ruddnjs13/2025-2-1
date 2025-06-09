using System;
using System.Collections.Generic;
using UnityEngine;

namespace _01.Code.Tower
{
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "SO/UpgradeData", order = 0)]
    public class towerDataSO : ScriptableObject
    {
        public int maxUpgradeCount = 3;
        public List<int> priceList = new List<int>(3); 
        public List<int> damageList = new List<int>(3); 
        public int damage;
        public bool skillEnabled;

        private void OnEnable()
        {
            damage = damageList[0];
        }
    }
}