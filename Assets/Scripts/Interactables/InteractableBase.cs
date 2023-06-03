using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        protected bool canInteract = true;

        public virtual void Interact(PlayerController caller)
        {
            //Do whatever has to happen in child class then clear
            caller.ClearInteractableInRange(this);
            canInteract = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(canInteract && other.CompareTag("Player"))
                other.GetComponent<PlayerController>().SetInteractableInRange(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
                other.GetComponent<PlayerController>().ClearInteractableInRange(this);
        }
    }
}