using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
	[SerializeField] Transform turretTarget;
	[SerializeField] Transform turretHead;
	[SerializeField] Transform turretShootPoint;
	[SerializeField] SphereCollider triggerCollider;

	[SerializeField] private float rotationSpeed = 5f;
	[SerializeField] private bool isInteractable;

	private bool targetDetected = false;

	private void OnTriggerEnter(Collider other)
	{
		if(other.transform.gameObject == turretTarget.gameObject)
		{
			targetDetected = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if(other.transform.gameObject == turretTarget.gameObject)
		{
			targetDetected = false;
			//Standby();
		}
	}

	private void Update()
	{
		if(targetDetected)
		{
			RotateTowardsTarget();
		}
		else
		{
			Standby();
		}
	}

	private void RotateTowardsTarget()
	{
		Debug.Log("will rotate towards target");
		if(turretTarget != null)
		{
			Vector3 targetDirection = turretTarget.position - turretHead.position;
			targetDirection.y = 0f;
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
			turretHead.rotation = Quaternion.Slerp(turretHead.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
	}
	private void Standby()
	{
		Debug.Log("target has left the trigger area");
		Quaternion initialRotation = Quaternion.identity;
		turretHead.rotation = Quaternion.Slerp(turretHead.rotation, initialRotation, rotationSpeed * Time.deltaTime);
	}


}
