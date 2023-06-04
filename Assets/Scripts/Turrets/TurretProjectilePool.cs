using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.AI
{
	public class TurretProjectilePool : MonoBehaviour
	{
		public static TurretProjectilePool instance;

		public GameObject projectilePrefab;
		public List<GameObject> pooledObjects;
		public int countToPool = 20;

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			pooledObjects = new List<GameObject>();
			for (int i = 0; i < countToPool; i++)
			{
				GameObject instantiatedObject = Instantiate(projectilePrefab);
				instantiatedObject.SetActive(false);
				pooledObjects.Add(instantiatedObject);
			}
		}

		public GameObject GetPooledObject()
		{
			for (int i = 0; i < pooledObjects.Count; i++)
			{
				if (!pooledObjects[i].activeInHierarchy)
				{
					return pooledObjects[i];
				}
			}
			return null;
		}
	}

}
