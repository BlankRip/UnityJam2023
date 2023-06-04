using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Gameplay
{
    public class InteractText : MonoBehaviour
    {
        public static InteractText Instance;
        private void Awake()
        {
            if(Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        [SerializeField] GameObject parentTransform;
        [SerializeField] TextMeshProUGUI instructionText;

        public void ShowText(string instruction)
        {
            instructionText.SetText(instruction);
            parentTransform.SetActive(true);
        }

        public void HideText()
        {
            parentTransform.SetActive(false);
        }
    }
}