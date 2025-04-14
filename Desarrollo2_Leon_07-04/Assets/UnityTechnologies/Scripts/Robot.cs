using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Robot : MonoBehaviour
{
    private ForceRequest _instantForceRequest;
    private ForceRequest _constantForceRequest;
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void RequestInstantForce(ForceRequest request)
    {
        _instantForceRequest = request;
    }

    public void RequestConstantForce(ForceRequest request)
    {
        _constantForceRequest = request;
    }

    private void FixedUpdate()
    {
        if (_constantForceRequest == null)
        {
            return;
        }
        else
        {

        }

        if (_instantForceRequest == null)
        {
            return;
        }
        else
        {
            rigidBody.AddForce(_instantForceRequest.direction * _instantForceRequest.force, ForceMode.Impulse);
            _instantForceRequest = null;
        }
    }

}
