using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
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

    // holds last stored position and rotation of anchor
    private Vector3 lastAnchoredPosition;
    private Quaternion lastAnchoredRotation;

    // text element to show text on screen
    private Text debuggerInfo;

    // text to show on the screen
    private string debugText;

    // Start is called before the first frame update
    void Start()
    {
        // TODO exchange to our own overlay
        FitToScanOverlay.SetActive(true);
        this.debuggerInfo = GameObject.Find("DebuggerConsole").GetComponentInChildren<Text>();
        this.Logger("test");
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.alreadySetAnchor)
        {
            // get all pictures from camera
            Session.GetTrackables<AugmentedImage>(
                m_TempAugmentedImages, TrackableQueryFilter.Updated);

            foreach (var image in m_TempAugmentedImages)
            {
                if (image.TrackingState == TrackingState.Tracking)
                {
                    // we only set anchor, when a picture was found a picture to track
                    this.SetAnchor(image);
                    FitToScanOverlay.SetActive(false);
                    this.alreadySetAnchor = true;
                }

            }
        }
        this.CheckAnchorDrift();
        this.debuggerInfo.text = this.debugText;
    }

    public void SetAnchor(AugmentedImage image)
    {
        // create anchor where image was scanned
         this.anchor = image.CreateAnchor(image.CenterPose);

        // place image where the image was scanned
        this.poster.transform.position = image.CenterPose.position;
        this.poster.transform.Rotate(0, 90, 0);
        //this.poster.transform.rotation = image.CenterPose.rotation;
        this.poster.SetActive(true);

        this.lastAnchoredPosition = anchor.transform.position;
        this.lastAnchoredRotation = anchor.transform.rotation;

        this.Logger("Anchor set");
    }

    // log how much the anchor moved from starting position
    public void CheckAnchorDrift()
    {
        if (anchor == null)
            return;

        if (lastAnchoredPosition != anchor.transform.position)
        {
            this.Logger("Distance Changed: " + Vector3.Distance(anchor.transform.position, lastAnchoredPosition));
            lastAnchoredPosition = anchor.transform.position;
        }

        if (lastAnchoredRotation != anchor.transform.rotation)
        {
            this.Logger("Rotation Changed: " + Quaternion.Angle(anchor.transform.rotation, lastAnchoredRotation));
            lastAnchoredRotation = anchor.transform.rotation;
        }
    }

    // Adds text to the debug view on the screen
    // TODO bring this to work...
    public void Logger(string text)
    {
        this.debugText = this.debugText + "\n" + text;
    }
}
