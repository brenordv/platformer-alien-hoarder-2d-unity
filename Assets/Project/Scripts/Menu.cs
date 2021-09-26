using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class Menu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene(0);
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
