using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        [SerializeField] protected string instructions = "Press 'Interact Key' to Use Item";
        protected bool canInteract = true;

        public virtual void Interact(PlayerController caller)
        {
            //Do whatever has to happen in child class then clear
            ClearInteractableFromPlayer(caller);
            canInteract = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(canInteract && other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().SetInteractableInRange(this);
                InteractText.Instance.ShowText(instructions);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
                ClearInteractableFromPlayer(other.GetComponent<PlayerController>());
        }

        protected void ClearInteractableFromPlayer(PlayerController player)
        {
            InteractText.Instance.HideText();
            player.ClearInteractableInRange(this);
        }
    }
}