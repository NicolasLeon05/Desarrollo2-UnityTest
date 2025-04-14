using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float _speed;
    [SerializeField] private float _force;

    private void OnEnable()
    {
        if (moveAction == null)
            return;

        moveAction.action.performed += OnMove;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        var request = new ForceRequest();

        var horizontalInput = obj.ReadValue<Vector2>();
        request.direction = new Vector3 (horizontalInput.x, 0, horizontalInput.y);
        request.speed = _speed;
        request.force = _force;

        character.RequestInstantForce(request);
    }

}