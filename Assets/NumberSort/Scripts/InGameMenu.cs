using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaximovInk.NumbersSort
{
    public class InGameMenu : MonoBehaviour
    {
        public GameObject pausePanel;

        public void HomeAction()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }

        public void Pause()
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }

        public void UnPause()
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }

    }
}