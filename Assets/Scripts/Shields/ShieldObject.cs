using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
	public class ShieldObject : InteractableBase
	{
		[SerializeField] int shieldIncreaseAmount;
		[SerializeField] AudioData pickUpAudio;

		public override void Interact(PlayerController caller)
		{
			caller.GetComponent<PlayerShieldHandler>().IncreaseShieldAmount(shieldIncreaseAmount);
			ClearInteractableFromPlayer(caller);
			Destroy(this.gameObject);
			Sound2D.Instance.PlayOneShotAudio(pickUpAudio);
		}
	}
}