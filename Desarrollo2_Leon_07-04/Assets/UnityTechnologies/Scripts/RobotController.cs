using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotController : MonoBehaviour
{
    [SerializeField] Robot player;
    [SerializeField] InputActionReference moveAction;

    [SerializeField] float _speed;
    [SerializeField] float _force;

    private void OnEnable()
    {
        if (moveAction == null)
            return;

        moveAction.action.performed += OnMove;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        var request = new ForceRequest();
        var horizontalInput = ctx.ReadValue<Vector2>();

        request.direction = new Vector3(horizontalInput.x, 0, horizontalInput.y);
        request.speed = _speed;
        request.force = _force;

        player.RequestInstantForce(request);
    }
}
