using UnityEngine;
using UnityEngine.SceneManagement;

namespace _01.Code.UI
{
    public class GameClearUI : MonoBehaviour
    {
        [SerializeField] private GameObject gameClearPanel;
        [SerializeField] private GameObject wavePanel;
        [SerializeField] private GameObject playerPanel;
        [SerializeField] private GameObject buildPanel;
        [SerializeField] private GameObject settingsPanel;

        public void GameClear()
        {
            Time.timeScale = 0;
            wavePanel.SetActive(false);
            playerPanel.SetActive(false);
            buildPanel.SetActive(false);
            settingsPanel.SetActive(false);
            gameClearPanel.SetActive(true);
        }

        public void ToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }
}