using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] GameObject pausePanel;

        private InputAction pauseAction;

        private void Start()
        {
            pauseAction = InputProvider.GetPlayerInput().actions["Pause"];
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if(pauseAction.WasReleasedThisFrame())
            {
                if(pausePanel.activeSelf)
                    ResumeGame();
                else
                    PauseGame();
            }    
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pausePanel.SetActive(false);
        }

        public void ReturnToMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
