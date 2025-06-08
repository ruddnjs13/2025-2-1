using UnityEngine;
using UnityEngine.Serialization;

namespace _01.Code.UI
{
    public class GameClearUI : MonoBehaviour
    {
        [SerializeField] private GameObject gameClearPanel;
        [SerializeField] private GameObject wavePanel;
        [SerializeField] private GameObject playerPanel;
        [SerializeField] private GameObject buildPanel;

        public void GameClear()
        {
            Time.timeScale = 0;
            wavePanel.SetActive(false);
            playerPanel.SetActive(false);
            buildPanel.SetActive(false);
            gameClearPanel.SetActive(true);
        }
    }
}