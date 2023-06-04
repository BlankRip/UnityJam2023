using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UI
{
    public class VictoryScreen : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 1;
        }

        public void ReturnToMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}