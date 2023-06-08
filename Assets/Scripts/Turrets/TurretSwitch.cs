using Gameplay;
using Gameplay.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.LevelGeneration;
using System.Linq;

namespace Gameplay.AI
{
    public class TurretSwitch : InteractableBase
    {
		[SerializeField] TurretMaterialObject turretMaterailsList;
		[SerializeField] Renderer baseObjRendrer;
		[SerializeField] AudioData turretPowerDownAudio;
		
		[Header("Only assign for debugging")]
		[SerializeField] private TurretController linkedTurret;

		public override void Interact(PlayerController caller)
		{
			linkedTurret.DeActivate();
			base.Interact(caller);
			Sound2D.Instance.PlayOneShotAudio(turretPowerDownAudio);
		}
		public void SetUpSwitch(TurretController turret)
		{
			linkedTurret = turret;
			if(turretMaterailsList != null && turretMaterailsList.materials.Count > 0)
			{
				int randomIndex = MazeGenerator.GetRandomFromRange(0, turretMaterailsList.materials.Count);
				Material randomMaterial = turretMaterailsList.materials[randomIndex];

				baseObjRendrer.material = randomMaterial;
				linkedTurret.SetTurretColor(randomMaterial);

				turretMaterailsList.materials.RemoveAt(randomIndex);
			}
		}
	}
}
