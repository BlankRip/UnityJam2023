using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShieldHandler : MonoBehaviour
{
	[SerializeField] Slider shieldSlider;

	[Header("Shield Attributes")]
	[SerializeField] private int shieldDepletionAmount = 1;
	[SerializeField] private int maxShield = 100;

	private int currentShield;

	private int shieldDamageAmount = 10;

	private bool shieldCanBeActivated;

	InputAction ShieldTriggerInput;

	private void Start()
	{
		PlayerInput input = InputProvider.GetPlayerInput();
		ShieldTriggerInput = input.actions["Shields"];

		shieldCanBeActivated = true;
		shieldSlider.maxValue = maxShield;
		currentShield = maxShield;
		shieldSlider.value = currentShield;
	}

	private void Update()
	{
		if(shieldCanBeActivated && ShieldTriggerInput.IsPressed())
		{
			currentShield -= shieldDepletionAmount;
			currentShield = Mathf.Max(currentShield, 0);
			shieldSlider.value = currentShield;
		}
	}

	public void ShieldTakeDamage()
	{
		if(shieldCanBeActivated && currentShield >= 1)
		{
			currentShield -= shieldDamageAmount;
			currentShield = Mathf.Max(currentShield, 0);
			shieldSlider.value = currentShield;
		}
	}

	public void IncreaseShieldAmount(int regenAmount)
	{
		if(currentShield != maxShield) 
		{ 
			int newShieldValue = currentShield + regenAmount;
			currentShield = Mathf.Min(newShieldValue, maxShield);
			shieldSlider.value = currentShield;
		}
	}

	public void SetShieldDamageAmount(int damageAmount)
	{
		shieldDamageAmount = damageAmount;
	}

	public bool GetShieldStatus()
	{
		return shieldCanBeActivated;
	}
}
