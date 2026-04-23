using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void GoMainMenu()
        {
            SceneManager.LoadScene(1);
        }
    }
}
