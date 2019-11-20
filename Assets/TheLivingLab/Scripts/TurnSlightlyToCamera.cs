using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

// Turns an object always to camera.
public class TurnSlightlyToCamera : MonoBehaviour
{
    // holds the information point
    public GameObject objectToTurn;

    // camera of the scene to look at
    public Camera camera;

    // Update is called once per frame
    void Update()
    {
        // source: https://answers.unity.com/questions/36255/lookat-to-only-rotate-on-y-axis-how.html
        Vector3 targetPostition = new Vector3(camera.transform.position.x,
                                       this.transform.position.y,
                                       camera.transform.position.z);
        this.transform.LookAt(targetPostition);

        //TODO make object turn only a certain degrees from starting position, for example 45 degrees
    }
}
