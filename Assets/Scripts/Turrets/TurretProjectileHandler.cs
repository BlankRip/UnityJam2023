using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.UI;

namespace Gameplay.AI
{
	public class TurretProjectileHandler : MonoBehaviour
	{
		public float lifetime;
		private float spawnTime;

		private void OnEnable()
		{
			spawnTime = Time.time;
		}

		private void OnCollisionEnter(Collision other)
		{
			if(other.gameObject.CompareTag("Player"))
			{
				this.gameObject.SetActive(false);
				GameOver.Instance.EndGame();
			}
		}

		private void Update()
		{
			if (Time.time - spawnTime >= lifetime)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
