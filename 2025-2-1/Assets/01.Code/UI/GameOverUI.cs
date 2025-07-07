using UnityEngine;
using UnityEngine.SceneManagement;

namespace _01.Code.UI
{
    public class GameOverUI : MonoBehaviour
    {
        public void Retry()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}