using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.UI
{
    public class GameOver : MonoBehaviour
    {
        public static GameOver Instance;
        private void Awake()
        {
            if(Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        [SerializeField] GameObject gameOverPanel;

        public void EndGame()
        {
            if(!gameOverPanel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
            }
        }

        public void Retry()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Sound2D.Instance.PlayButtonAudio();
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
            Sound2D.Instance.PlayButtonAudio();
        }
    }
}