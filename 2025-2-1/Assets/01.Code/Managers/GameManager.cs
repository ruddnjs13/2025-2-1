using System;
using Core.GameEvent;
using Settings.InputSettings;
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
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private InputReaderSO inputReader;
        
        private void Start()
        {
            systemChannel.AddListener<LifeDownEvent>(HandleLifeDown);
        }

        private void OnDestroy()
        {
            systemChannel.RemoveListener<LifeDownEvent>(HandleLifeDown);
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
            inputReader.RockInput(false);
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
            playerPanel.SetActive(false);
            buildPanel.SetActive(false);
            settingsPanel.SetActive(false);
        }
    }
}