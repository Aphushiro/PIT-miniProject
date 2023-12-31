using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTankSettings : MonoBehaviour
{
    public GameObject debugPoint, debugPointRed;
    [SerializeField] float boxMaxLength = 15;
    [SerializeField] float boxMaxHeight = 6;

    public Vector3 getRandomPosWithinBoundary() // Gets a random points within a boundary. The y is always above 2 so fish don't swim into the sand.
    {
        Vector3 ranPos = new Vector3(Random.Range(-boxMaxLength, boxMaxLength), Random.Range(1f, boxMaxHeight), Random.Range(-boxMaxLength, boxMaxLength));
        //  Instantiate(debugPoint, ranPos, Quaternion.identity);
        return ranPos;
    }
}
