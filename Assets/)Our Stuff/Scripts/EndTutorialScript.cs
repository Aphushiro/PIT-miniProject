using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorialScript : MonoBehaviour
{

    public MeshRenderer meshrenderer;
    public Material captureMat;

    public bool hasChanged;


    public void EndTutorial()
    {
        meshrenderer.material = captureMat;
    }
}
