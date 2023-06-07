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
        [SerializeField] ScriptableInt seed;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void PlayButton()
        {
            controlesPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
            Sound2D.Instance.PlayButtonAudio();
        }

        public void QuitButton()
        {
            Sound2D.Instance.PlayButtonAudio();
            Application.Quit();
        }

        public void StartGameButton()
        {
            seed.value = Random.Range(int.MinValue, int.MaxValue);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Sound2D.Instance.PlayButtonAudio();
        }

        public void BackToMenu()
        {
            mainMenuPanel.SetActive(true);
            controlesPanel.SetActive(false);
            Sound2D.Instance.PlayButtonAudio();
        }
    }
}