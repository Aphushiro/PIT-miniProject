using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMatSetup : MonoBehaviour
{
    public Camera cameraB;

    public Material matB;

    void Start()
    {
        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }
        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        matB.mainTexture = cameraB.targetTexture;
    }
}
