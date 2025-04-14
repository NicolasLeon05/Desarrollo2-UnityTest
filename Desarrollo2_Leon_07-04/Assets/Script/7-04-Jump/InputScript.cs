using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rigidBody;

    [SerializeField] private float speed = 20f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float holdJumpForce = 8f;
    [SerializeField] private float maxJumpHoldTime = 1f;
    [SerializeField] private int maxJumps = 2;

    private Vector3 _moveInput = Vector3.zero;
    private bool isJumpRequested = false;
    private bool isJumpHeld = false;
    private bool isJumping = false;
    private float jumpTimer = 0.5f;
    private int jumps = 0;

    public void FixedUpdate()
    {
        rigidBody.AddForce(_moveInput * speed, ForceMode.Force);

        if (jumps <= maxJumps)
        {
            if (isJumpRequested)
            {
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = true;
                jumpTimer = 0f;
                isJumpRequested = false;
            }

            if (isJumping && isJumpHeld && jumpTimer < maxJumpHoldTime)
            {
                jumpTimer += Time.fixedDeltaTime;
                rigidBody.AddForce(Vector3.up * holdJumpForce, ForceMode.Force);
            }

            if (!isJumpHeld || jumpTimer >= maxJumpHoldTime)
            {
                isJumping = false;
            }
        }
    }

    public void OnPlayerCollision()
    {
        jumps = 0;
    }

    private void OnEnable()
    {
        moveAction.action.started += HandleMoveInput;
        moveAction.action.performed += HandleMoveInput;
        moveAction.action.canceled += HandleMoveInput;

        jumpAction.action.started += HandleJumpStarted;
        jumpAction.action.performed += HandleJumpHeld;
        jumpAction.action.canceled += HandleJumpReleased;
    }

    private void HandleJumpStarted(InputAction.CallbackContext ctx)
    {
        if (jumps < maxJumps)
        {
            isJumpRequested = true;
            isJumpHeld = true;
            jumpTimer = 0f;
            jumps++;
        }
    }

    private void HandleJumpHeld(InputAction.CallbackContext ctx)
    {
        isJumpHeld = true;
    }

    private void HandleJumpReleased(InputAction.CallbackContext ctx)
    {
        isJumpHeld = false;
    }

    private void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        _moveInput.x = input.x;
        _moveInput.z = input.y;
    }
}
