using System;
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
        if (_constantForceRequest != null)
        {
            rigidBody.AddForce(_constantForceRequest.direction * _constantForceRequest.speed, ForceMode.Force);

            if (rigidBody.linearVelocity.magnitude > _constantForceRequest.speed)
            {

                rigidBody.linearVelocity = rigidBody.linearVelocity.normalized * _constantForceRequest.speed;
            }

            Console.WriteLine(rigidBody.linearVelocity);
        }

        if (_instantForceRequest != null)
        {
            rigidBody.AddForce(_instantForceRequest.direction * _instantForceRequest.force, ForceMode.Impulse);
            _instantForceRequest = null;
        }

    }

}
