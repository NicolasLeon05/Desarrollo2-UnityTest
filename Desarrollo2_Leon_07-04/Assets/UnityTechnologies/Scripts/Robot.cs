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
        //Move
        if (_constantForceRequest != null)
        {
            rigidBody.AddForce(_constantForceRequest.direction * _constantForceRequest.speed, ForceMode.Force);

            if (rigidBody.linearVelocity.magnitude > _constantForceRequest.speed)
            {
                rigidBody.linearVelocity = rigidBody.linearVelocity.normalized * _constantForceRequest.speed;
            }
        }

        //Jump
        if (_instantForceRequest != null)
        {
            if (rigidBody.linearVelocity.y == 0)
            {
                rigidBody.AddForce(_instantForceRequest.direction * _instantForceRequest.force, ForceMode.Impulse);
                _instantForceRequest = null;
                //Debug.Log("jump");
            }
        }

        Debug.Log(rigidBody.linearVelocity);
    }

}
