using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
	public class ShieldObject : MonoBehaviour
	{
		[SerializeField] int shieldIncreaseAmount;

		private void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.CompareTag("Player"))
			{
				PlayerShieldHandler playerShield =  other.gameObject.GetComponent<PlayerShieldHandler>();
				playerShield.IncreaseShieldAmount(shieldIncreaseAmount);
			}
		}
	}
}