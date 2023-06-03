using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
	public class PlayerMovement : MonoBehaviour
	{
		InputAction playerMove;
		[SerializeField] CharacterController characterController;

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
			//characterController = GetComponent<CharacterController>();

			playerMove = input.actions["Move"];

			if(gravity > 0)
			{
				gravity *= -1f;
			}
		}
		private void Update()
		{
			//GroundCheck();
			Movement();
		}

		//private void GroundCheck()
		//{
		//	isGrounded = characterController.isGrounded;
		//	if(isGrounded && gravityDirection.y < 0)
		//	{
		//		gravityDirection.y = 0;
		//	}
		//}

		private void Movement()
		{
			Vector2 moveInput = playerMove.ReadValue<Vector2>();

			Debug.Log(moveInput);

			horizontalInput = moveInput.x;
			verticalInput = moveInput.y;

			playerDirection = transform.forward * verticalInput + transform.right * horizontalInput;
			characterController.Move(playerDirection * currentSpeed * Time.deltaTime);
		}
	}
}
