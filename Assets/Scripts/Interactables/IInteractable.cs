using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public interface IInteractable
    {
        void Interact(PlayerController caller);
    }
}