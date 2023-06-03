using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
	[SerializeField] GameObject turretTarget;
	[SerializeField] SphereCollider triggerCollider;
	[SerializeField] Transform turretShootPoint;

	[SerializeField] private float rotationSpeed = 5f;
	[SerializeField] private bool isInteractable;

	private bool targetDetected = false;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == turretTarget)
		{
			targetDetected = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject == turretTarget)
		{
			targetDetected = false;
			Standby();
		}
	}

	private void Update()
	{
		if(targetDetected)
		{
			RotateTowardsTarget();
		}
	}

	private void RotateTowardsTarget()
	{
		Debug.Log("will rotate towards target");
	}
	private void Standby()
	{
		Debug.Log("target has left the trigger area");
	}


}
