using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public GameObject FitToScanOverlay;

    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

    public Camera firstPersonCamera;

    private Anchor anchor;
    public GameObject anchoredPicture;

    private bool alreadySetAnchor = false;

    private Vector3 lastAnchoredPosition;
    private Quaternion lastAnchoredRotation;

    private Text debuggerInfo;

    

    // Start is called before the first frame update
    void Start()
    {
        FitToScanOverlay.SetActive(true);
        debuggerInfo = GameObject.Find("DebuggerConsole").GetComponentInChildren<Text>();
        debuggerInfo.text = "";
    }

    // Update is called once per frame
    void Update()
    {   
        if (!this.alreadySetAnchor)
        {
            // Get updated augmented images for this frame.
            Session.GetTrackables<AugmentedImage>(
                m_TempAugmentedImages, TrackableQueryFilter.Updated);

            foreach (var image in m_TempAugmentedImages)
            {
                // move camera in front of picture
                //this.firstPersonCamera.transform.position = image.CenterPose.position;
                //this.firstPersonCamera.transform.rotation = new Quaternion(0, 270f, 0, 0); // TODO don't know if this is the way

                this.SetAnchor(image);
                this.alreadySetAnchor = true;
                FitToScanOverlay.SetActive(false);
                break;
            }
        }
        //this.CheckAnchorDrift();
    }

    public void SetAnchor(AugmentedImage image)
    {
        this.alreadySetAnchor = true;

        // create anchor
        //this.anchor = Session.CreateAnchor(image.CenterPose);
        this.anchor = image.CreateAnchor(image.CenterPose); // TODO adjust rotation

        // make an object child of the anchor to see where it is
        // picture will move to where the anchor will move
        /* only do this, when we want to create a new instance of the picture!
            GameObject.Instantiate(anchoredPicture,
            anchor.transform.position,
            anchor.transform.rotation,
            anchor.transform);
        */

        lastAnchoredPosition = anchor.transform.position;
        lastAnchoredRotation = anchor.transform.rotation;
    }


    public void CheckAnchorDrift()
    {
        if (anchor == null)
            return;

        if (lastAnchoredPosition != anchor.transform.position)
        {
            Log("Distance Changed: " + Vector3.Distance(anchor.transform.position, lastAnchoredPosition));
            lastAnchoredPosition = anchor.transform.position;
        }

        if (lastAnchoredRotation != anchor.transform.rotation)
        {
            Log("Rotation Changed: " + Quaternion.Angle(anchor.transform.rotation, lastAnchoredRotation));
            lastAnchoredRotation = anchor.transform.rotation;
        }
    }

    public void Log(string text)
    {
        this.debuggerInfo.text = this.debuggerInfo.text + "\n" + text;
    }

}
