using Gameplay;
using Gameplay.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.LevelGeneration;
using System.Linq;

namespace Gameplay.AI
{
    public class TurretSwitch : InteractableBase
    {
		[SerializeField] private TurretMaterialObject turretMaterailsList;
		private TurretController linkedTurret;

		public override void Interact(PlayerController caller)
		{
			linkedTurret.DeActivate();
			base.Interact(caller);
		}
		public void SetLinkedTurret(TurretController turret)
		{
			linkedTurret = turret;
		}
		public void SetupColor()
		{
			if(turretMaterailsList != null && turretMaterailsList.materials.Count > 0)
			{
				int randomIndex = MazeGenerator.GetRandomFromRange(0, turretMaterailsList.materials.Count);
				Material randomMaterial = turretMaterailsList.materials[randomIndex];

				GetComponent<Renderer>().material = randomMaterial;
				linkedTurret.SetTurretColor(randomMaterial);

				turretMaterailsList.materials.RemoveAt(randomIndex);
			}
		}
	}
}
