using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class ExitPortal : InteractableBase
    {
        public override void Interact(PlayerController caller)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            base.Interact(caller);
        }
    }
}