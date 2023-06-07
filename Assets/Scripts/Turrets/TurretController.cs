using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.AI
{
    public class TurretController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Transform turretHead;
        [SerializeField] Transform turretShootPoint;
        [SerializeField] SphereCollider triggerCollider;

        [Header("Turret main attributes")]
        [SerializeField] private float rotationSpeed = 5f;

        [Header("Turret shoot attributes")]
        [SerializeField] private GameObject turretProjectilePrefab;
        [SerializeField] LayerMask raycastLayerMask;
        [SerializeField] private float turretProjectileSpeed;
        [SerializeField] private float turretTimeBetweenShots;

        private bool targetDetected = false;
		private bool poweredUp = true;

        Transform turretTarget;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.gameObject.CompareTag("Player"))
            {
                turretTarget = other.transform;
                targetDetected = true;
                TurretShoot();
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
            }
            else
            {
                Standby();
            }
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
            StartCoroutine(ShootCoroutine());
        }

        IEnumerator ShootCoroutine()
        {
            while(targetDetected && poweredUp) 
            {
                if(turretShootPoint != null && triggerCollider != null)
                {
					float maxRaycastDistance = triggerCollider.radius;
                    RaycastHit hit;
                    Vector3 rayDirection = turretTarget.position - turretHead.position;

                    if (Physics.Raycast(turretShootPoint.position, rayDirection, out hit, maxRaycastDistance, raycastLayerMask))
                    {
                        if(hit.collider.CompareTag("Player"))
                            {
							    GameObject projectileObject = TurretProjectilePool.instance.GetPooledObject();

							    projectileObject.transform.position = turretShootPoint.position;
							    projectileObject.transform.rotation = Quaternion.identity;

							    Rigidbody bulletRb = projectileObject.GetComponent<Rigidbody>();

							    bulletRb.velocity = turretShootPoint.forward * turretProjectileSpeed;
							    projectileObject.SetActive(true);
						}
                    }
                }
                yield return new WaitForSeconds(turretTimeBetweenShots / 100f);
            }
        }

        public void SetTurretColor(Material turretMat)
        {
            turretHead.gameObject.GetComponent<Renderer>().material = turretMat;
        }

		public void DeActivate()
		{
			poweredUp = false;
		}
    }
}