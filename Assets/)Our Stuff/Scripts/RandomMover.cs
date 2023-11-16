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

    IEnumerator randomPointPicker;
    Rigidbody _rigidBody;
    Vector3 _currentDirection, _rotatedDirection;
    float _singleStep, originalRotateSpeed, originalSwimSpeed;
    
    void Start()
    {
        fishTankSettings = FindObjectOfType<FishTankSettings>();
        originalRotateSpeed = rotateSpeed;
        originalSwimSpeed = swimSpeed;
        randomPointPicker = RandomPointPicker(timeBetweenPickingNewPoint);
        StartCoroutine(randomPointPicker);
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _singleStep = rotateSpeed * Time.deltaTime;

        _currentDirection = (currentPoint - transform.position).normalized;


        _rotatedDirection = Vector3.RotateTowards(transform.forward, _currentDirection, _singleStep,0);
        transform.rotation = Quaternion.LookRotation(_rotatedDirection);
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
        rotateSpeed = originalRotateSpeed;
    }
    private void StopFindingPoints()
    {
        swimSpeed = 0;
    }
}
