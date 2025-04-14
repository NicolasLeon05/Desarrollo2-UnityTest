using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private GameObject player;

    [SerializeField] Rigidbody rigidBody;

    private Vector3 _moveInput = new Vector3(0, 0, 0);

    [SerializeField] private int speed = 20;
    [SerializeField] private int jumpForce = 10;

    private bool isJumpRequested;
    private bool isJumpPressed = false;

    private int jumps = 0;
    private int maxJumps = 2;
    private int maxJumpMultiplier = 2;


    /*public void Update()
    {
        player.GetComponent<Transform>().position += _moveInput;
    }
    */
    public void FixedUpdate()
    {
        rigidBody.AddForce(_moveInput * speed, ForceMode.Force);

        if (isJumpRequested && jumps <= maxJumps)
        {
            if (isJumpPressed)
            {

            }
            isJumpRequested = false;
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void OnPlayerCollision()
    {
        jumps = 0;
        Debug.Log("toca el piso");
    }

    private void OnEnable()
    {
        moveAction.action.started += HandleMoveInput;
        moveAction.action.performed += HandleMoveInput;
        moveAction.action.canceled += HandleMoveInput;

        jumpAction.action.started += HandleJumpInput;
    }

    private void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        isJumpRequested = true;
        jumps++;
        Debug.Log("Jump " + jumps);
    }

    private void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        _moveInput.x = ctx.ReadValue<Vector2>().x;
        _moveInput.z = ctx.ReadValue<Vector2>().y;

        Debug.Log(ctx.ReadValue<Vector2>());
    }

}
