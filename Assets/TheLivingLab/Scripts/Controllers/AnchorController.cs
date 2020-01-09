using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class AnchorController : MonoBehaviour
{
    // temporarily stored pictures to find poster
    private List<AugmentedImage> recognisedImages = new List<AugmentedImage>();

    // holds anchor of scanned poster
    private Anchor anchor;

    // holds last stored position and rotation of anchor
    private Vector3 lastAnchoredPosition;
    private Quaternion lastAnchoredRotation;

    // access to onboarding methods
    public OnboardingController onboarding;

    // access to scene controller methods
    public SceneController scene;

    // true when user is about to look for poster
    private bool isLookingForPoster = false;

    // default scan time for poster
    private float SCAN_TIME_DEFAULT = 4f;

    // amount of seconds scanned poster
    private int scanTimer = 0;

    // true when scan timer was started
    private bool isScanTimerStarted = false;

    private AugmentedImage scannedImage;

    // Update is called once per frame
    void Update()
    {
        if (this.isLookingForPoster && Session.Status == SessionStatus.Tracking)
        {
            Session.GetTrackables<AugmentedImage>(
                recognisedImages, TrackableQueryFilter.Updated);

            // loop over pictures from camera
            foreach (var image in recognisedImages)
            {
                /*
                if (!isScanTimerStarted)
                {
                    isScanTimerStarted = true;
                    StartCoroutine("IncreaseScanTimer");
                    onboarding.ShowWaitingOverlay();
                }*/

                if (image.TrackingState == TrackingState.Tracking)
                {
                    scannedImage = image;
                    // set anchor to persist the picture points around this object
                    SetAnchor();
                    // align the rest of AMIs world according to poster
                    syncTheWorld();
                    this.isLookingForPoster = false;
                    onboarding.DisableScanOverlay();
                }

            }
        }
        //this.LogAnchorDrift();

        // set poster always to anchor, because anchor can drifft, but can also be corrected by arcore device
        if (anchor != null)
        {
            scene.poster.transform.position = this.anchor.transform.position;
            //scene.poster.transform.rotation = this.anchor.transform.rotation;
        }
    }

    // Set anchor to center of scanned image.
    public void SetAnchor()
    {
        // create anchor where image was scanned
        this.anchor = scannedImage.CreateAnchor(scannedImage.CenterPose);

        // keep the last position and rotation
        this.lastAnchoredPosition = anchor.transform.position;
        this.lastAnchoredRotation = anchor.transform.rotation;
    }

    // Position AMIs world according to poster.
    private void syncTheWorld()
    {
        scene.poster.transform.position = scannedImage.CenterPose.position;

        // we rotate AMIs world with 90 degrees "backwards", so it is flat at the wall
        // side rotation we don't rotate so the poster has always same rotation
        Quaternion imageRotation = scannedImage.CenterPose.rotation;
        this.scene.poster.transform.rotation = imageRotation;

        // rotates the poster because scanned image rotation is flat
        this.scene.poster.transform.Rotate(90, 0, 0);

        // rotates poster with world because we see the backside of it
        // IMPORTANT: you have to adjust this when moving poster to other place
        this.scene.poster.transform.Rotate(0, 180, 0);

        this.scene.EnableAMIsWorld();
    }

    // Log how much the anchor driffted from starting position.
    // TODO we could adjust poster to correct drift!
    public void LogAnchorDrift()
    {
        if (anchor == null)
            return;

        if (lastAnchoredPosition != anchor.transform.position)
        {
            Debug.Log("Distance Changed: " + Vector3.Distance(anchor.transform.position, lastAnchoredPosition));
            lastAnchoredPosition = anchor.transform.position;
        }

        if (lastAnchoredRotation != anchor.transform.rotation)
        {
            Debug.Log("Rotation Changed: " + Quaternion.Angle(anchor.transform.rotation, lastAnchoredRotation));
            lastAnchoredRotation = anchor.transform.rotation;
        }
    }

    // Enables AnchorController to look for poster.
    public void LookForPoster()
    {
        this.isLookingForPoster = true;
    }

    // Increases scan timer
    IEnumerator IncreaseScanTimer()
    {
        while (true)
        {
            float DEFAULT_WAIT = 1;
            yield return new WaitForSeconds(DEFAULT_WAIT);
            scanTimer++;
        }
    }
}
