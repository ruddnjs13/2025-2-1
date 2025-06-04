using _01.Code.Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace _01.Code.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Image fill;

        public void SetHealthBar(int amount, Enemy owner)
        {
            fill.fillAmount = 1 / (owner.enemyData.maxHealth) * owner.Health;
        }
    }
}