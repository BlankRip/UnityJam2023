using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
	public class PlayerMovement : MonoBehaviour
	{
		InputAction playerMove;
		CharacterController characterController;

		[SerializeField] Transform playerMesh;
		[SerializeField] private float moveSpeed;
		[SerializeField] private float gravity;

		private bool isGrounded;
		private float currentSpeed;
		private float horizontalInput, verticalInput;
		private Vector3 playerDirection;
		private Vector3 gravityDirection;
		private void Start()
		{
			PlayerInput input = InputProvider.GetPlayerInput();
			characterController = GetComponent<CharacterController>();

			currentSpeed = moveSpeed;

			playerMove = input.actions["Move"];

			if(gravity > 0)
			{
				gravity *= -1f;
			}
		}
		private void Update()
		{
			GroundCheck();
			Movement();
		}

		private void GroundCheck()
		{
			isGrounded = characterController.isGrounded;
			if(isGrounded && gravityDirection.y < 0f)
			{
				gravityDirection.y = -2f;
			}
			gravityDirection.y += gravity * Time.deltaTime;
			characterController.Move(gravityDirection * Time.deltaTime);
		}

		private void Movement()
		{
			Vector2 moveInput = playerMove.ReadValue<Vector2>();

			horizontalInput = moveInput.x;
			verticalInput = moveInput.y;

			playerDirection = transform.forward * verticalInput + transform.right * horizontalInput;
			characterController.Move(playerDirection * currentSpeed * Time.deltaTime);

			if(playerDirection != Vector3.zero)
			{
				Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
				playerMesh.rotation = Quaternion.Lerp(playerMesh.rotation, targetRotation, Time.deltaTime * 10f);
			}
		}
	}
}
