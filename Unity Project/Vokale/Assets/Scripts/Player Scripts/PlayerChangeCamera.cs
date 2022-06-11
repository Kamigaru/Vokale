using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeCamera : MonoBehaviour
{
    bool isFPS = true;

    public Camera fpsCamera;
    public Camera p3Camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F3))
        {
            if (isFPS)
            {
                fpsCamera.enabled = false;
                p3Camera.enabled = true;
                
                isFPS = false;
            }
            else
            {
                fpsCamera.enabled = true;
                p3Camera.enabled = false;
            }
        }
    }
}
