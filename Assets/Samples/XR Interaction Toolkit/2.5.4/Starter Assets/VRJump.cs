using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRJump : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpButton; // Changed to InputActionReference for better management in the Editor
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private CharacterController cc;
    [SerializeField] private LayerMask groundLayers;

    private float gravity = -9.81f; // Standard gravity value should be negative
    private Vector3 movement;

    private void OnEnable()
    {
        jumpButton.action.Enable(); // Enable the action
    }

    private void OnDisable()
    {
        jumpButton.action.Disable(); // Disable the action
    }

    private void Update()
    {
        bool isGrounded = IsGrounded();

        // Update the downward movement based on gravity each frame
        if (isGrounded && movement.y < 0)
        {
            movement.y = 0f; // Neutralize downward speed when grounded
        }

        if (jumpButton.action.WasPressedThisFrame() && isGrounded)
        {
            Jump(); // Correct the function name to have uppercase as per C# conventions
        }

        // Apply gravity over time if not grounded
        movement.y += gravity * Time.deltaTime;
        cc.Move(movement * Time.deltaTime); // Apply the movement to the character controller
    }

    private bool IsGrounded()
    {
        // Check if the bottom of the object is near the ground
        return Physics.CheckSphere(transform.position, 0.2f, groundLayers);
    }

    private void Jump()
    {
        // Calculate the velocity needed to achieve the jump height
        movement.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
