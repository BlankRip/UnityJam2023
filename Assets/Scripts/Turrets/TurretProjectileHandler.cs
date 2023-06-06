using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
	public class TurretProjectileHandler : MonoBehaviour
	{
		public float lifetime;
		private float spawnTime;

		private bool dealDamage = false;

		private void OnEnable()
		{
			spawnTime = Time.time;
		}

		private void OnCollisionEnter(Collision other)
		{
			if(other.gameObject.CompareTag("Player"))
			{
				dealDamage = true;
				this.gameObject.SetActive(false);
				dealDamage = false;
			}
		}

		private void OnCollisionExit(Collision other) 
		{
			if(other.gameObject.CompareTag("Player"))
			{
				this.gameObject.SetActive(false);
				dealDamage= false;
			}
		}

		private void Update()
		{
			if (Time.time - spawnTime >= lifetime)
			{
				gameObject.SetActive(false);
			}
		}

		public bool GetDamageTriggerStatus()
		{
			return dealDamage;
		}
	}
}
