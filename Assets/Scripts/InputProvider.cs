using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class InputProvider : MonoBehaviour
    {
        private static PlayerInput playerInput;

		private void Awake()
		{
            if(playerInput == null)
            {
			    playerInput = GetComponent<PlayerInput>();
                DontDestroyOnLoad(this.gameObject);
            }
            else
                Destroy(this.gameObject);
		}

		public static PlayerInput GetPlayerInput()
        {
            if(playerInput == null)
                Debug.LogError("Player input is not set, either not on scene or component not available on GO");
            return playerInput;
        }
    }
}
