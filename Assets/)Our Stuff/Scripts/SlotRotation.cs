using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotRotation : MonoBehaviour
{
    public Transform camTransform;

    void Update()
    {
        float yRot = camTransform.position.y;
        Vector3 slotRot = new Vector3(0, yRot, 0);
        transform.rotation.SetEulerAngles(slotRot);
    }
}
