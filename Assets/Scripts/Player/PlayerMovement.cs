using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    CharacterController controller;

    [SerializeField] float walkSpeed = 0.1f;
    [SerializeField] float jumpHeight = 1.0f;

    float horizontalInput;
    float verticalInput;

    Vector3 playerVelocity;
    Vector3 moveDir;

    InputActionAsset actions;

    float gravityValue = -9.81f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        actions = GetComponent<PlayerInput>().actions;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!controller.isGrounded) return;
        playerVelocity.y += jumpHeight;
    }

    void Update()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 input = actions.FindAction("Move").ReadValue<Vector2>();
        moveDir = transform.forward * input.y + transform.right * input.x;

        controller.Move(moveDir.normalized * walkSpeed * Time.deltaTime);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
