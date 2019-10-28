using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

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

    public GameObject anchorDisplay;

    // true when anchor of poster was set
    private bool alreadySetAnchor = false;

    // holds last stored position and rotation of anchor
    private Vector3 lastAnchoredPosition;
    private Quaternion lastAnchoredRotation;

    // holds all information points at stations
    public List<GameObject> informationPoints;

    // holds all arrows
    public List<GameObject> arrows;

    // Start is called before the first frame update
    void Start()
    {
        this.SetupScene();
        // TODO exchange to our own overlay
        FitToScanOverlay.SetActive(true);
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
                    this.alreadySetAnchor = true;
                    FitToScanOverlay.SetActive(false);
                }

            }
        }
        this.LogAnchorDrift();

        // set poster always to anchor
        if (this.anchor != null)
        {
            this.poster.transform.position = this.anchor.transform.position;
        }
    }

    // set the anchor to center of scanned image
    public void SetAnchor(AugmentedImage image)
    {
        // create anchor where image was scanned
        this.anchor = image.CreateAnchor(image.CenterPose);

        this.lastAnchoredPosition = anchor.transform.position;
        this.lastAnchoredRotation = anchor.transform.rotation;
        this.syncTheWorld();
    }

    // position the rest of AMIs world according to picture
    private void syncTheWorld()
    {
        // TODO add success message to screen when scan was successfull
        GameObject.Instantiate(this.anchorDisplay,
            anchor.transform.position,
            anchor.transform.rotation,
            anchor.transform);

        // place image where the image was scanned
        // rest of the other world is also placed, because they are all children
        // TODO only works when started with camera facing the poster to scann
        //this.poster.transform.LookAt(camera.transform);
        //this.poster.transform.Rotate(0, 0,0);
        this.poster.SetActive(true);

        // show information points
        this.SetActiveInfoPoints(true);
    }

    // hide all objects from scene
    private void SetupScene()
    {
        this.poster.SetActive(false);
        foreach(var arrow in this.arrows) {
            arrow.SetActive(false);
        }
        this.SetActiveInfoPoints(false);
    }

    private void SetActiveInfoPoints(bool isActive)
    {
        foreach(var point in this.informationPoints)
        {
            point.SetActive(isActive);
        }
    }

    // log how much the anchor moved from starting position
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
