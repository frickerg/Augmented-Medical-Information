using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

/*
 * Guides the user after app start to give access to camera,
 * use head phone and scan the poster.
 */
public class OnboardingController : MonoBehaviour
{
    // the overlay to say user to scan the picture
    public GameObject FitToScanOverlay;

    // overlay to say user to wait while scanning
    public GameObject FitToScanWaitOverlay;

    // is shown at start and is removed as soon as user granted permission
    public GameObject NoPermissionOverlay;

    // access to methods of AnchorController
    public AnchorController anchor;

    // true when user granted camera permission, stops checking for camera permission
    private bool isCameraPermissionGranted = false;

    // GameObject that holds all GUI-Elements for the Onboarding
    public GameObject Onboarding;

    // overlay when user had not granted camera permission
    public GameObject CameraPermission_withoutanimation;

    // ui for user to set volume
    public GameObject VolumeOverlay;

    // holds Google ARCore camera to activate when scanning
    public GameObject ARCoreCamera;

    // holds camera active on startup
    public GameObject StartingCamera;

    // Start is called before the first frame update
    void Start()
    {
        // check for error in beginning
        this.QuitOnConnectionErrors();
    }

    void Update()
    {
        if (!this.isCameraPermissionGranted)
        {
            this.CheckForCameraPermission();
        }
    }

    // Hides the Onboarding ui.
    // Is called from button on last onboarding step.
    public void HideOnboarding()
    {
        Onboarding.SetActive(false);
        StartPosterScan();
    }

    // Quits the app.
    // Button action when user gave no camera permission.
    public void QuitApplication()
    {
        Application.Quit();
    }

    // Shows overlay to hold still while scanning.
    public void ShowWaitingOverlay()
    {
        DisableScanOverlay();
        FitToScanWaitOverlay.SetActive(true);
    }

    // Hides hold still scan overlay.
    public void DisableScanWaitOverlay()
    {
        FitToScanWaitOverlay.SetActive(false);
    }

    // method to enable camera and start ARCore Session 
    public void StartRoomScan()
    {
        StartingCamera.SetActive(false);
        ARCoreCamera.SetActive(true);
    }

    // Shows message that user should
    // scan the poster to start AMIs experience.
    // Tells Anchor controller to look for poster.
    private void StartPosterScan()
    {
        FitToScanOverlay.SetActive(true);
        this.anchor.LookForPoster();
    }

    // Hides the scan overlay.
    // Is called from AnchorController when poster was scanned.
    public void DisableScanOverlay()
    {
        this.FitToScanOverlay.SetActive(false);
    }

    // Hides no camera permisison overlay
    public void DisableCameraOverlay()
    {
        this.CameraPermission_withoutanimation.SetActive(false);
    }

    // Catches errors
    void QuitOnConnectionErrors()
    {
        if (Session.Status.IsError())
        {
            // This covers a variety of errors.  See reference for details
            // https://developers.google.com/ar/reference/unity/namespace/GoogleARCore
            Debug.Log("Something is broken!");

            //TODO show a message
        }
    }

    // Check for camera permission and check overlay
    private void CheckForCameraPermission()
    {
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            // show this when camera permission is not granted
            this.Onboarding.SetActive(false);
            this.CameraPermission_withoutanimation.SetActive(true);
        } else if (Session.Status == SessionStatus.Tracking)
        {
            this.CameraPermission_withoutanimation.SetActive(false);
            this.isCameraPermissionGranted = true;
        }
    }
}
