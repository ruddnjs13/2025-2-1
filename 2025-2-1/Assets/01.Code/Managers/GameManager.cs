using System;
using Core.GameEvent;
using UnityEngine;

namespace _01.Code.Managers
{
    public class GameManager : MonoSingleton<GameManager
    >
    {
        [field:SerializeField] public int HealthPoint { get; private set; } = 5;

        [SerializeField] private GameEventChannelSO systemChannel;
        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private GameObject buildPanel;
        [SerializeField] private GameObject playerPanel;
        
        private void Start()
        {
            systemChannel.AddListener<LifeDownEvent>(HandleLifeDown);
        }

        private void HandleLifeDown(LifeDownEvent evt)
        {
            HealthPoint--;
            if (HealthPoint <= 0)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
            playerPanel.SetActive(false);
            buildPanel.SetActive(false);
        }
    }
}