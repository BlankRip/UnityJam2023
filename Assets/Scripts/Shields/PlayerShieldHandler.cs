using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShieldHandler : MonoBehaviour
{
	[Header("Shield Attributes")]
	[SerializeField] private float shieldDepletionAmount = 1;
	[SerializeField] private float maxShield = 100;

	private Slider shieldSlider;

	private float currentShield;


	InputAction ShieldTriggerInput;

	private void Start()
	{
		PlayerInput input = InputProvider.GetPlayerInput();
		ShieldTriggerInput = input.actions["Shields"];

		shieldSlider = GameObject.FindGameObjectWithTag("Shield").GetComponent<Slider>();
		shieldSlider.maxValue = maxShield;
		currentShield = maxShield;
		shieldSlider.value = currentShield;
	}

	private void Update()
	{
		if(currentShield > 0.0f && ShieldTriggerInput.IsPressed())
		{
			currentShield -= shieldDepletionAmount * Time.deltaTime;
			currentShield = Mathf.Max(currentShield, 0);
			shieldSlider.value = currentShield;
		}
	}

	public void ShieldTakeDamage(float damage)
	{
		if(currentShield >= 1)
		{
			currentShield -= damage;
			currentShield = Mathf.Max(currentShield, 0);
			shieldSlider.value = currentShield;
		}
	}

	public void IncreaseShieldAmount(int regenAmount)
	{
		if(currentShield != maxShield) 
		{ 
			float newShieldValue = currentShield + regenAmount;
			currentShield = Mathf.Min(newShieldValue, maxShield);
			shieldSlider.value = currentShield;
		}
	}
}
