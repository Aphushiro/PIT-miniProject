using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticController : MonoBehaviour
{
    public XRBaseController leftcontroller, rightcontroller;
    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 0.5f;

    [ContextMenu("Send Haptics")]
    public void SendHaptics()
    {
        leftcontroller.SendHapticImpulse(defaultAmplitude, defaultDuration);
        rightcontroller.SendHapticImpulse(defaultAmplitude, defaultDuration);
    }

    public void HapticCam ()
    {
        SendHaptics();
    }
}
