using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShieldHandler : MonoBehaviour
{
	[Header("Shield Attributes")]
	[SerializeField] private GameObject shieldGameObject;
	[SerializeField] private float shieldDepletionAmount = 1;
	[SerializeField] private float maxShield = 100;

	private Slider shieldSlider;

	private float currentShield;
	private float sizeLerpTime;

	Vector3 initialSize;
	Vector3 targetSize;

	InputAction ShieldTriggerInput;

	private void Start()
	{
		PlayerInput input = InputProvider.GetPlayerInput();
		ShieldTriggerInput = input.actions["Shields"];

		shieldSlider = GameObject.FindGameObjectWithTag("Shield").GetComponent<Slider>();
		shieldSlider.maxValue = maxShield;
		currentShield = maxShield;
		shieldSlider.value = currentShield;

		initialSize = shieldGameObject.transform.localScale;
		targetSize = new Vector3(4.36f, 4.36f, 4.36f);

		shieldGameObject.SetActive(false);
	}

	private void Update()
	{
		if(currentShield > 0.0f && ShieldTriggerInput.IsPressed())
		{
			shieldGameObject.SetActive(true);

			ShieldOn();

			currentShield -= shieldDepletionAmount * Time.deltaTime;
			currentShield = Mathf.Max(currentShield, 0);
			shieldSlider.value = currentShield;


		}
		else if (currentShield <= 0.0f && ShieldTriggerInput.WasReleasedThisFrame() || !ShieldTriggerInput.IsPressed())
		{
			ShieldOff();
			shieldGameObject.SetActive(false);
		}
	}

	private void ShieldOn()
	{
		sizeLerpTime += Time.deltaTime;
		shieldGameObject.transform.localScale = Vector3.Lerp(initialSize, targetSize, sizeLerpTime);
	}

	private void ShieldOff()
	{
		sizeLerpTime += Time.deltaTime;
		shieldGameObject.transform.localScale = Vector3.Lerp(targetSize, initialSize, sizeLerpTime);
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
