using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{

    private static bool secondDisplayActivated = false;

    private void Start()
    {
        if (Display.displays.Length > 1)
        {
            if (!secondDisplayActivated)
            {
                Display.displays[1].Activate(1920, 1080, 60);
                secondDisplayActivated = true;
            }

            GetComponent<Camera>().SetTargetBuffers(
                Display.displays[1].colorBuffer,
                Display.displays[1].depthBuffer);
        }

    }
}
