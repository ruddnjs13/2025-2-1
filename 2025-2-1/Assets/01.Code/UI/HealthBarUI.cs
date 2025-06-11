using System;
using _01.Code.Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace _01.Code.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image border;
        [SerializeField] private Image fill;


        public void ResetHealthBar()
        {
            fill.fillAmount = 1f;
        }
        private void OnEnable()
        {
            fill.fillAmount = 1;
            EnableHealthBar(true);
        }

        public void SetHealthBar(int health, Enemy owner)
        {
            fill.fillAmount = (1 / (float)owner.enemyData.maxHealth) * health;
        }

        public void EnableHealthBar(bool isActive = false)
        {
            border.gameObject.SetActive(isActive);
        }
    }
}