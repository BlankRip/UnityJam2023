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
	[SerializeField] float lerpSpeed = 10;

	[SerializeField] AudioData onAudio;
	[SerializeField] AudioData offAudio;

	private Slider shieldSlider;

	private float currentShield;

	private bool lerpComplete;

	Vector3 initialSize, activeSize;
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
		activeSize = new Vector3(4.36f, 4.36f, 4.36f);

		lerpComplete = true;
	}

	private void Update()
	{
		if(currentShield > 0.0f && ShieldTriggerInput.IsPressed())
		{
			if(ShieldTriggerInput.WasPressedThisFrame())
			{
				targetSize = activeSize;
				lerpComplete = false;
				Sound2D.Instance.PlayOneShotAudio(onAudio);
			}

			currentShield -= shieldDepletionAmount * Time.deltaTime;
			currentShield = Mathf.Max(currentShield, 0);
			shieldSlider.value = currentShield;
		}
		else if (ShieldTriggerInput.WasReleasedThisFrame())
		{
			targetSize = initialSize;
			lerpComplete = false;
			Sound2D.Instance.PlayOneShotAudio(offAudio);
		}

		if(lerpComplete)
			return;
		shieldGameObject.transform.localScale = Vector3.Lerp(shieldGameObject.transform.localScale , targetSize, lerpSpeed * Time.deltaTime);
		float distance = (shieldGameObject.transform.localScale - targetSize).sqrMagnitude;
		if(distance < 0.03 * 0.03)
			lerpComplete = true;
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
