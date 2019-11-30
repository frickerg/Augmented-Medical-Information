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

    public GameObject TestButtonInfo;

    // is shown at start and is removed as soon as user granted permission
    public GameObject NoPermissionOverlay;

    // access to methods of AnchorController
    public AnchorController anchor;

    // true when user granted camera permission, stops checking for camera permission
    private bool isCameraPermissionGranted = false;

    // GameObject that holds all GUI-Elements for the Onboarding
    public GameObject Onboarding;

    //public CanvasGroup _canvasGroup;

    public GameObject CameraPermission_withoutanimation;


    // Start is called before the first frame update
    //ev. in Awake-Methode
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
        this.ShowScanPosterMessage();
       // _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }


    // Shows message that user should
    // scan the poster to start AMIs experience.
    private void ShowScanPosterMessage()
    {
        // TODO exchange to our own overlay
        FitToScanOverlay.SetActive(true);
        TestButtonInfo.SetActive(true);
        this.anchor.LookForPoster();
    }

    // Hides the scan overlay.
    // Is called from AnchorController when poster was scanned.
    public void DisableScanOverlay()
    {
        this.FitToScanOverlay.SetActive(false);
    }

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
        } else if (Session.Status == SessionStatus.Tracking || Session.Status == SessionStatus.Initializing)
        {
            // camera permission was given
            this.CameraPermission_withoutanimation.SetActive(false);
            this.Onboarding.SetActive(true);
            this.isCameraPermissionGranted = true;

            //this.Onboarding.SetActive(true);
        }
    }
}
