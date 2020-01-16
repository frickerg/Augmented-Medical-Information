using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class AnchorController : MonoBehaviour
{
    // temporarily stored pictures to find poster
    private List<AugmentedImage> trackableImages;

    // holds anchor of scanned poster
    private Anchor anchor = null;

    // holds last stored position and rotation of anchor
    private Vector3 lastAnchoredPosition;
    private Quaternion lastAnchoredRotation;

    // access to onboarding methods
    public OnboardingController onboarding;

    // access to scene controller methods
    public SceneController scene;

    // default scan time for poster
    private float SCAN_TIME_DEFAULT = 5f;

    // amount of seconds scanned poster
    private int scanTimePast = 0;

    // true when scan timer was started
    private bool isScanTimerStarted = false;

    // true when poster is scanned first time, need to know for next scans that it doesn't show hold still
    private bool isPosterScannedFirstTime = true;

    // holds scanned images that are used to calculate new position
    private List<AugmentedImage> tempFoundPosterImage;

    private void Start()
    {
        tempFoundPosterImage = new List<AugmentedImage>();
        trackableImages = new List<AugmentedImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Session.Status == SessionStatus.Tracking && anchor == null)
        {
            // only check for images when no anchor and when tracking

            // Get updated augmented images for this frame.
            Session.GetTrackables<AugmentedImage>(
                trackableImages, TrackableQueryFilter.Updated);

            if (trackableImages.Count > 0)
            {
                if (isPosterScannedFirstTime)
                {
                    // only show waiting at first scan
                    isPosterScannedFirstTime = false;
                    onboarding.ShowWaitingOverlay();
                }

                if (!isScanTimerStarted)
                {
                    // start the timer when not yet started
                    isScanTimerStarted = true;
                    StartCoroutine("IncreaseScanTimer");
                }

                if (scanTimePast < SCAN_TIME_DEFAULT)
                {
                    // add all found images to array list
                    foreach (var image in trackableImages)
                    {
                        if (image.TrackingState == TrackingState.Tracking)
                        {
                            tempFoundPosterImage.Add(image);
                        }
                    }
                }
            }

            if (scanTimePast >= SCAN_TIME_DEFAULT)
            {
                StopCoroutine("IncreaseScanTimer");
                isScanTimerStarted = false;
                scanTimePast = 0;
                if (tempFoundPosterImage.Count > 0)
                {
                    calculateNewPosterPosition();
                }
            }
        } else if (Session.Status != SessionStatus.Tracking)
        {
            /*
            //TODO need to fix this, currently if (trackableImages.Count > 0) is also bigger 0, when poster is not in sight, but probably not in trackng mode...
            // reset anchor and poster to null and tell user to scan poster again
            scene.SetVisibilityOfAMIsWorld(false);
            this.anchor = null;
            scene.poster.transform.position = new Vector3(0,0,0);
            scene.poster.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.onboarding.ShowScanOverlay();
            isPosterScannedFirstTime = true;
            isScanTimerStarted = false;*/
        }
        //this.LogAnchorDrift();
    }

    private void calculateNewPosterPosition()
    {
        float totalPosX = 0, totalPosY = 0, totalPosZ = 0;
        float totalRotX = 0, totalRotY = 0, totalRotZ = 0, totalRotW = 0;

        foreach (var image in tempFoundPosterImage)
        {
            Vector3 position = image.CenterPose.position;
            totalPosX += position.x;
            totalPosY += position.y;
            totalPosZ += position.z;

            Quaternion rotation = image.CenterPose.rotation;
            totalRotX += rotation.x;
            totalRotY += rotation.y;
            totalRotZ += rotation.z;
            totalRotW += rotation.w;
        }

        // calculate new position and rotation
        int imageCount = tempFoundPosterImage.Count;
        Vector3 newPosition = new Vector3(totalPosX / imageCount, totalPosY / imageCount, totalPosZ / imageCount);
        Quaternion newRotation = new Quaternion(totalRotX / imageCount, totalRotY/imageCount, totalRotZ/imageCount, totalRotW/imageCount);

        scene.poster.transform.position = newPosition;
        scene.poster.transform.rotation = newRotation;

        // rotates the poster because scanned image rotation is flat
        scene.poster.transform.Rotate(90, 0, 0);

        // rotates poster with world because we see the backside of it
        // IMPORTANT: you have to adjust this when moving poster to other place
        scene.poster.transform.Rotate(0, 180, 0);

        // new anchor with poster position and rotation, parent from poster
        Pose pose = new Pose(scene.poster.transform.position, this.scene.poster.transform.rotation);
        anchor = Session.CreateAnchor(pose);
        scene.poster.transform.parent = anchor.transform;

        // hide wait overlay
        onboarding.DisableScanWaitOverlay();

        // show poster and AMIs world
        scene.SetVisibilityOfAMIsWorld(true);

        // rest temp poster list;
        tempFoundPosterImage = new List<AugmentedImage>();
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

    // Increases scan timer
    IEnumerator IncreaseScanTimer()
    {
        while (true)
        {
            float DEFAULT_WAIT = 1;
            yield return new WaitForSeconds(DEFAULT_WAIT);
            scanTimePast++;
        }
    }
}
