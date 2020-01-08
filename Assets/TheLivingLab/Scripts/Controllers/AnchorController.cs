using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class AnchorController : MonoBehaviour
{
    // temporarily stored pictures to find poster
    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

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

    // Update is called once per frame
    void Update()
    {
        if (this.isLookingForPoster)
        {
            // get all pictures from camera, normally 60 fps, TODO set this specificly
            Session.GetTrackables<AugmentedImage>(
                m_TempAugmentedImages, TrackableQueryFilter.Updated);

            // loop over pictures from camera
            foreach (var image in m_TempAugmentedImages)
            {
                if (image.TrackingState == TrackingState.Tracking)
                {
                    // we found a picture that was in AugmentedImage database!
                    Debug.Log("We found the picture");

                    // set anchor to persist the picture points around this object
                    // (other recognised picture points are thrown away to stay performant)
                    this.SetAnchor(image);
                    // align the rest of AMIs world according to poster
                    this.syncTheWorld(image);
                    this.isLookingForPoster = false;
                    onboarding.DisableScanOverlay();
                }

            }
        }
        //this.LogAnchorDrift();

        // set poster always to anchor, because anchor can drifft, but can also be corrected by arcore device
        if (this.anchor != null)
        {
            this.scene.poster.transform.position = this.anchor.transform.position;
            // we don't want to touch rotation, otherwise it will turn around...
        }
    }

    // Set anchor to center of scanned image.
    public void SetAnchor(AugmentedImage image)
    {
        // create anchor where image was scanned
        this.anchor = image.CreateAnchor(image.CenterPose);

        // keep the last position and rotation
        this.lastAnchoredPosition = anchor.transform.position;
        this.lastAnchoredRotation = anchor.transform.rotation;
    }

    // Position AMIs world according to poster.
    private void syncTheWorld(AugmentedImage scannedImage)
    {
        // we rotate AMIs world with 90 degrees "backwards", so it is flat at the wall
        // side rotation we don't rotate so the poster has always same rotation
        Quaternion imageRotation = scannedImage.CenterPose.rotation;
        this.scene.poster.transform.rotation = imageRotation;
        this.scene.poster.transform.Rotate(90, 0, 0);

        // we rotate poster with world because we see the backside of it
        // IMPORTANT: probably you have to adjust this when moving poster to other place
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
}
