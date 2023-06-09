using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Gameplay.UI;

namespace Gameplay
{
    public class DebugingScript : MonoBehaviour
    {
        private InputAction debugAction;
        [SerializeField] AudioData testData;
        [SerializeField] GameObject obj;

        private void Start()
        {
            debugAction = InputProvider.GetPlayerInput().actions["X"];
        }

        private void Update()
        {
            if(debugAction.WasReleasedThisFrame())
                obj.SetActive(!obj.activeSelf);
                //Sound2D.Instance.PlayOneShotAudio(testData);
        }
    }
}