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

    // is shown at start and is removed as soon as user granted permission
    public GameObject NoPermissionOverlay;

    // access to methods of AnchorController
    public AnchorController anchor;

    // true when user granted camera permission, stops checking for camera permission
    private bool isCameraPermissionGranted = false;

    // GameObject that holds all GUI-Elements for the Onboarding
    public GameObject Onboarding; 

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

    public void HideOnboarding()
    {
        Onboarding.SetActive(false);
        this.ShowScanRoomMessage();
    }

    // Shows message that user should use earphones for a
    // more immersive experience.
    private void ShowEarphonesMessage()
    {
        // TODO
        // click button enables next step
        this.ShowScanRoomMessage();
    }

    // Shows message that user should scan
    // room a bit to calibrate AMI.
    private void ShowScanRoomMessage()
    {
        //TODO when scanned (distance to ultimate center is smaller than 0.5 meters) start next step
        this.ShowScanPosterMessage();
    }

    // Shows message that user should
    // scan the poster to start AMIs experience.
    private void ShowScanPosterMessage()
    {
        // TODO exchange to our own overlay
        FitToScanOverlay.SetActive(true);
        this.anchor.LookForPoster();
    }

    // Hides the scan overlay.
    // Is called from AnchorController when poster was scanned.
    public void DisableScanOverlay()
    {
        this.FitToScanOverlay.SetActive(false);
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
            this.NoPermissionOverlay.SetActive(true);
        } else if (Session.Status == SessionStatus.Tracking)
        {
            this.NoPermissionOverlay.SetActive(false);
            this.isCameraPermissionGranted = true;
            //this.Onboarding.SetActive(true);
        }
    }
}
