using System;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Character : MonoBehaviour
{
    private ForceRequest _instantForceRequest;
    private ForceRequest _continuousForceRequest;
    private Rigidbody _rigidBody;

    public void RequestInstantForce(ForceRequest forceRequest)
    {
        _instantForceRequest = forceRequest;
    }

    public void RequestContinuousForce(ForceRequest forceRequest)
    {
        _continuousForceRequest = forceRequest;
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(_continuousForceRequest != null)
            _rigidBody.AddForce(_continuousForceRequest.direction * _instantForceRequest.force, ForceMode.Force);


        if (_instantForceRequest == null)
            return;

        _rigidBody.AddForce(_instantForceRequest.direction * _instantForceRequest.force, ForceMode.Impulse);
        _instantForceRequest = null;
    }

}
