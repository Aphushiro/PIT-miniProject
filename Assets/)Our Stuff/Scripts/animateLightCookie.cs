using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class animateLightCookie : MonoBehaviour
{
    private UniversalAdditionalLightData lightExtantion;
    public Vector2 offsetVector = new Vector2(0.1f,0.1f);

    private void Awake()
    {
        lightExtantion = GetComponent<UniversalAdditionalLightData>();
    }

    private void Update()
    {
        lightExtantion.lightCookieOffset += offsetVector * Time.deltaTime;
    }
}
