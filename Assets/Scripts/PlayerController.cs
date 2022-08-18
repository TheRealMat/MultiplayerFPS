using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    CharacterController controller;

    [SerializeField]
    float walkSpeed = 0.1f;
    [SerializeField]
    float jumpHeight = 1.0f;

    float horizontalInput;
    float verticalInput;

    Vector3 playerVelocity;
    Vector3 moveDir;

    float gravityValue = -9.81f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDir = transform.forward * verticalInput + transform.right * horizontalInput;
        controller.Move(moveDir.normalized * walkSpeed * Time.deltaTime);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            if (!controller.isGrounded) return;
            playerVelocity.y += Mathf.Sqrt(jumpHeight);
        }
    }
}
