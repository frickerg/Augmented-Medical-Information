using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

/*
 * Controlls AMIs world after onboarding was made.
 * OnboardingController should activate this controller.
 */
public class SceneController : MonoBehaviour
{
    // the overlay to say user to scann the picture
    public GameObject FitToScanOverlay;

    // temporarily stored pictures to find poster
    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();
    
    // holds anchor of scanned poster
    private Anchor anchor;
    
    // poster in the virtual world
    public GameObject poster;

    // true when anchor of poster was set
    private bool alreadySetAnchor = false;

    // holds all information points at stations
    public List<GameObject> informationPoints;

    // holds all arrows
    public List<GameObject> arrows;

    // holds last stored position and rotation of anchor
    private Vector3 lastAnchoredPosition;
    private Quaternion lastAnchoredRotation;

    // Start is called before the first frame update.
    void Start()
    {
        //TODO start onboarding in other controller and then activate this controller

        this.SetupScene();
        // TODO exchange to our own overlay
        FitToScanOverlay.SetActive(true);
    }

    // Update is called once per frame.
    void Update()
    {
        if (!this.alreadySetAnchor)
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
                    this.alreadySetAnchor = true;
                    FitToScanOverlay.SetActive(false);
                }

            }
        }
        //this.LogAnchorDrift();

        // set poster always to anchor, because anchor can drifft, but can also be corrected by arcore device
        if (this.anchor != null)
        {
            this.poster.transform.position = this.anchor.transform.position;
            // we don't want to touch rotation, otherwise it will turn around...
        }

        // never let screen sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Set anchor to center of scanned image.
    public void SetAnchor(AugmentedImage image)
    {
        // create anchor where image was scanned
        this.anchor = image.CreateAnchor(image.CenterPose);

        // TODO add success message to screen when scan was successfull

        // keep the last position and rotation
        this.lastAnchoredPosition = anchor.transform.position;
        this.lastAnchoredRotation = anchor.transform.rotation;

        // align the rest of AMIs world according to poster
        this.syncTheWorld(image);
    }

    // Position AMIs world according to poster.
    private void syncTheWorld(AugmentedImage scannedImage)
    {
        // we rotate AMIs world with 90 degrees "backwards", so it is flat at the wall
        // side rotation we don't rotate so the poster has always same rotation
        Quaternion imageRotation = scannedImage.CenterPose.rotation;
        this.poster.transform.rotation = imageRotation;
        this.poster.transform.Rotate(90, 0, 0);

        // all objects of AMIs world are children of poster
        this.poster.SetActive(true);

        // show information points
        this.SetActiveInfoPoints(true);
    }

    // Hide all objects from scene.
    // should be called at startup
    private void SetupScene()
    {
        foreach(var arrow in this.arrows) {
            arrow.SetActive(false);
        }
        this.SetActiveInfoPoints(false);
        this.poster.SetActive(false);
    }

    // Activate/Deactivate all Infopoints.
    private void SetActiveInfoPoints(bool isActive)
    {
        foreach(var point in this.informationPoints)
        {
            point.SetActive(isActive);
        }
    }

    // Log how much the anchor driffted from starting position.
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
}
