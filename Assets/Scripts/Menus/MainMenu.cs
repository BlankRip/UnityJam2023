using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] GameObject mainMenuPanel;
        [SerializeField] GameObject controlesPanel;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void PlayButton()
        {
            controlesPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
        }

        public void QuitButton()
        {
            Application.Quit();
        }

        public void StartGameButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void BackToMenu()
        {
            mainMenuPanel.SetActive(true);
            controlesPanel.SetActive(false);
        }
    }
}