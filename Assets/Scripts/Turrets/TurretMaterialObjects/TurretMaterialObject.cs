using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
	[CreateAssetMenu(fileName = "Turret Material List", menuName = "Gameplay/Turret material list")]
	public class TurretMaterialObject : ScriptableObject
	{
		public List<Material> materials;

		public List<Material> InitialMaterialsList;

		public void ResetMaterialsList()
		{
			materials = new List<Material>(InitialMaterialsList);
		}
	}
}