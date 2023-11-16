using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))] //Video says this was best practice. As a rigidbody will automatically be added if not on the gameobject.
public class Swimmer : MonoBehaviour
{
    [SerializeField] float swimForce = 2;
    [SerializeField] float dragForce = 1;
    [SerializeField] float minForce;
    [SerializeField] float minTimeBetweenStrokes;

    [SerializeField] InputActionReference leftControllerSwimReference;
    [SerializeField] InputActionReference leftControllerVelocity;

    [SerializeField] InputActionReference rightControllerSwimReference;
    [SerializeField] InputActionReference rightControllerVelocity;
    [SerializeField] Transform trackingReference;

    Rigidbody _rigidbody;
    float _cooldownTimer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        _cooldownTimer += Time.deltaTime;
        if (_cooldownTimer > minTimeBetweenStrokes
            && leftControllerSwimReference.action.IsPressed()
            && rightControllerSwimReference.action.IsPressed())
        {
            var lefthandVelocity = leftControllerVelocity.action.ReadValue<Vector3>();
            var righthandVelocity = rightControllerVelocity.action.ReadValue<Vector3>();

            Vector3 localVelocity = lefthandVelocity + righthandVelocity;
            localVelocity *= -1;
            if (localVelocity.sqrMagnitude > minForce * minForce) //sqrMagnitude is a lot faster, performance wise.
            {
                Vector3 worldVelocity =  trackingReference.TransformDirection(localVelocity);
                _rigidbody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
                _cooldownTimer = 0;
            }
        }
        if(_rigidbody.velocity.sqrMagnitude > 0.01f)
        {
            _rigidbody.AddForce(-_rigidbody.velocity*dragForce, ForceMode.Acceleration);    
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
