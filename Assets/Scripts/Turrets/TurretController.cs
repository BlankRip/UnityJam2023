using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI
{
    public class TurretController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Transform turretTarget;
        [SerializeField] Transform turretHead;
        [SerializeField] Transform turretShootPoint;
        [SerializeField] SphereCollider triggerCollider;

        [Header("Turret main attributes")]
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private bool isInteractable;

        [Header("Turret shoot attributes")]
        [SerializeField] private GameObject turretProjectilePrefab;
        [SerializeField] private float turretProjectileSpeed;

        private bool targetDetected = false;
		private bool poweredUp = true;

		private UnityEngine.InputSystem.InputAction debugKey;
		private void Start()
		{
			debugKey = InputProvider.GetPlayerInput().actions["X"];
		}

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.gameObject.CompareTag("Player"))
            {
                targetDetected = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.gameObject.CompareTag("Player"))
            {
                targetDetected = false;
            }
        }

        private void Update()
        {
			if(!poweredUp)
				return;
			
            if (targetDetected)
            {
                RotateTowardsTarget();
                TurretShoot();
            }
            else
            {
                Standby();
            }

			if(debugKey.WasPressedThisFrame())
				DeActivate();
        }

        private void RotateTowardsTarget()
        {
            if (turretTarget != null)
            {
                Vector3 targetDirection = turretTarget.position - turretHead.position;
                targetDirection.y = 0f;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                turretHead.rotation = Quaternion.Slerp(turretHead.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        private void Standby()
        {
            Quaternion initialRotation = Quaternion.identity;
            turretHead.rotation = Quaternion.Slerp(turretHead.rotation, initialRotation, rotationSpeed * Time.deltaTime);
        }

        private void TurretShoot()
        {
            if (turretShootPoint != null && triggerCollider != null)
            {
                float maxRaycastDistance = triggerCollider.radius;
                RaycastHit hit;
                Vector3 rayDirection = turretTarget.position - turretShootPoint.position;

                if (Physics.Raycast(turretShootPoint.position, rayDirection, out hit, maxRaycastDistance))
                {
                    GameObject turretProjectile = Instantiate(turretProjectilePrefab, turretShootPoint.position, turretShootPoint.rotation);
                    Rigidbody projectileRB = turretProjectile.GetComponent<Rigidbody>();
                    if (projectileRB != null)
                    {
                        projectileRB.velocity = turretShootPoint.forward * turretProjectileSpeed;
						Destroy(turretProjectile, 1.5f);
                    }
                }
            }
        }

		public void DeActivate()
		{
			poweredUp = false;
		}
    }
}