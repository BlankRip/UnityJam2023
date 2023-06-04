using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShieldController : MonoBehaviour
{
	[SerializeField] Slider shieldSlider;

	private int currentShield;
	private int maxShield = 100;

	private int shieldDamageAmount = 10;

	private bool shieldActive;

	private void Start()
	{
		shieldActive = true;
		shieldSlider.maxValue = maxShield;
		currentShield = maxShield;
		shieldSlider.value = currentShield;
	}

	public void ShieldTakeDamage()
	{
		if(shieldActive && currentShield >= 1)
		{
			currentShield -= shieldDamageAmount;
			currentShield = Mathf.Max(currentShield, 0);
			shieldSlider.value = currentShield;
			Debug.Log(currentShield);
		}
	}

	public void SetShieldDamageAmount(int damageAmount)
	{
		shieldDamageAmount = damageAmount;
	}

	public bool GetShieldStatus()
	{
		return shieldActive;
	}
}
