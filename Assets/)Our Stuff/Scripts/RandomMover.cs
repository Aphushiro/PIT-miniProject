using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RandomMover : MonoBehaviour
{
    [SerializeField] FishTankSettings fishTankSettings;
    [SerializeField] Vector3 currentPoint; 
    [SerializeField] float swimSpeed = 3, rotateSpeed = 4;
    [SerializeField] float timeBetweenPickingNewPoint = 3;
    [SerializeField] bool pausedSwimming = false;

    IEnumerator randomPointPicker;
    Rigidbody _rigidBody;
    SphereCollider _sphereCollider;
    Vector3 _currentDirection, _rotatedDirection;
    float _singleStep, _originalRotateSpeed, _originalSwimSpeed;
    
    void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        fishTankSettings = FindObjectOfType<FishTankSettings>();
        _rigidBody = GetComponent<Rigidbody>();

        _originalRotateSpeed = rotateSpeed;
        _originalSwimSpeed = swimSpeed;
        randomPointPicker = RandomPointPicker(timeBetweenPickingNewPoint);
        StartCoroutine(randomPointPicker);
        StartCoroutine(nameof(StopMoving));

    }

    private void FixedUpdate()
    {
        _singleStep = rotateSpeed * Time.deltaTime;

        _currentDirection = (currentPoint - transform.position).normalized;

        if(pausedSwimming )
        {
            _currentDirection.y = 0;
            _rotatedDirection = Vector3.RotateTowards(transform.forward, _currentDirection, _singleStep, 0);
            transform.rotation = Quaternion.LookRotation(_rotatedDirection);
        }
        else
        {
            _rotatedDirection = Vector3.RotateTowards(transform.forward, _currentDirection, _singleStep, 0);
            transform.rotation = Quaternion.LookRotation(_rotatedDirection);
        }
        _rigidBody.AddForce(transform.forward * swimSpeed, ForceMode.Force);
    }

    private IEnumerator RandomPointPicker(float timeBetween) // Uses the "getRandomPosWithinBoundary" funbction to generate a random point every "timebetween" seconds.
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetween);
           
            currentPoint = fishTankSettings.getRandomPosWithinBoundary();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(pausedSwimming && other!= null)
        {
            StartFindingPoints();
        }
    }
    private void OnCollisionEnter(Collision collision) // if the fish hits something. This makes a new point in the other direction for it to travel to.
        //Also sets the rotate speed higher because otherwise the fish gets stuck.
        //Alternatively the collider could be disabled for a while.. hmm..
    {
        currentPoint =  transform.position + ((transform.position - collision.GetContact(0).point).normalized) * 15;
        rotateSpeed = 8;
        Invoke(nameof(ResetRotateSpeed), 1);
       Instantiate(fishTankSettings.debugPointRed, currentPoint, Quaternion.identity);
    }

    private void ResetRotateSpeed()
    {
        rotateSpeed = _originalRotateSpeed;
    }



    //--------------------------------------------- Making fish pause at places. So the player can more easily photograph them..
    private void StartFindingPoints()
    {
        StopCoroutine(randomPointPicker);
        swimSpeed = _originalSwimSpeed;
        pausedSwimming = false;
        _sphereCollider.enabled = false;
        StartCoroutine(nameof(StopMoving));

    }
    private void StopFindingPoints()
    {
        StopCoroutine(randomPointPicker);
        swimSpeed = 0;
        pausedSwimming = true;
        _sphereCollider.enabled = true;

    }
    private IEnumerator StopMoving() // Uses the "getRandomPosWithinBoundary" funbction to generate a random point every "timebetween" seconds.
    {

            yield return new WaitForSeconds(Random.Range(10,20));

            StopFindingPoints();
 
    }
}
