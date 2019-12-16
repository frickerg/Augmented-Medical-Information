using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

// Turns an object always to camera.
public class TurnToCamera : MonoBehaviour
{
    // camera of the scene to look at
    public Camera sceneCamera;

    // Update is called once per frame
    void Update()
    {
        // source: https://answers.unity.com/questions/36255/lookat-to-only-rotate-on-y-axis-how.html
        Vector3 targetPostition = new Vector3(sceneCamera.transform.position.x,
                                       this.transform.position.y,
                                       sceneCamera.transform.position.z);
        this.transform.LookAt(targetPostition);
    }
}
