using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

// Handles the state of connecting real world with augmented reality.
// 1) recognises if ARCore Device has scanned enough space of real world to start AR-experience.
//    We currently check for the SessionStatus of the ARCore Session. We also let user scan for specific timeout
//    Other option; detect surfaces and start as soon as we found one
// 2) shows a message to user when ARCore lost orientation and request to start scanning again
//    open question: does user have to scan poster again?
public class LocalizationController : MonoBehaviour
{
    // true when timeout is finished
    private bool isTimeoutFinished = false;

    // timeout in seconds to let user scan room at least
    private float timeout = 5f;

    // true when ARCore Sessions is ready for AR!
    public bool isLocalized = false;

    // true when started ascanning, prevent multiple calls to scan room
    private bool hasStartedScanningAgain = false;

    // start timeout as soon as activated
    void Start()
    {
        StartCoroutine("StartTimeout");
    }

    // set isLocalized true when timeout is finished and Session is tracking
    void Update()
    {
        if (Session.Status == SessionStatus.Tracking && isTimeoutFinished)
        {
            this.isLocalized = true;
            hasStartedScanningAgain = false;
            // TODO tell onboarding that it can go on
        } else if (Session.Status == SessionStatus.LostTracking)
        {
            if (!hasStartedScanningAgain)
            {
                hasStartedScanningAgain = true;
                // TODO show user to scan room again
                Debug.Log("Why did we lost tracking: " + Session.LostTrackingReason);
                this.isLocalized = false;
                this.isTimeoutFinished = false;
                StartCoroutine("StartTimeout");
            }
            
        } else if (Session.Status == SessionStatus.NotTracking)
        {
            Debug.Log("Currently not tracking. We can make a pause.");
        }
    }

    // Changes bool isTimeoutFinished to true after timeout
    IEnumerator StartTimeout()
    {
        yield return new WaitForSeconds(timeout);
        isTimeoutFinished = true;
    }
}
