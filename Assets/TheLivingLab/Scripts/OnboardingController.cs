using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

/*
 * Guides the user after app start to give access to camera,
 * use head phone and scan the poster.
 */
public class OnboardingController : MonoBehaviour
{
    // the overlay to say user to scan the picture
    public GameObject FitToScanOverlay;

    // access to methods of AnchorController
    public AnchorController anchor;

    // GameObject that holds all GUI-Elements for the Onboarding
    public GameObject Onboarding; 

    // Start is called before the first frame update
    void Start()
    {



        //TODO handle camera permission by ourselves to show proper message
        // Source: https://docs.unity3d.com/Manual/android-manifest.html
        //this.ShowEarphonesMessage();
        // check for errors
        this.QuitOnConnectionErrors();
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

    // Catches errors and tells user to give camera permission when not granted.
    void QuitOnConnectionErrors()
    {
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            StartCoroutine(CodelabUtils.ToastAndExit(
                "Camera permission is needed to run this application.", 5));
        }
        else if (Session.Status.IsError())
        {
            // This covers a variety of errors.  See reference for details
            // https://developers.google.com/ar/reference/unity/namespace/GoogleARCore
            StartCoroutine(CodelabUtils.ToastAndExit(
                "ARCore encountered a problem connecting. Please restart the app.", 5));
        }
    }
}
