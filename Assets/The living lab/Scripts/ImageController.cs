using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ImageController : MonoBehaviour
{

    Anchor anchor;
    public GameObject anchoredPicture;
    public GameObject unanchoredCube;

    Vector3 lastAnchoredPosition;
    Quaternion lastAnchoredRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateAnchor(AugmentedImage image)
    {
        // create anchor
        this.anchor = Session.CreateAnchor(image.CenterPose);

        // make an object child of the anchor to see where it is
        // picture will move to where the anchor will move
        GameObject.Instantiate(anchoredPicture, 
            anchor.transform.position, 
            anchor.transform.rotation, 
            anchor.transform);

        // display unanchored cube so we see drifft
        GameObject.Instantiate(unanchoredCube,
            anchor.transform.position,
            anchor.transform.rotation);
           
        // update last anchored position and rotation
        lastAnchoredPosition = anchor.transform.position;
        lastAnchoredRotation = anchor.transform.rotation;

        if (anchor == null)
            return;

        if (lastAnchoredPosition != anchor.transform.position)
        {
            Debug.Log("Distance Changed: "+ Vector3.Distance(anchor.transform.position, lastAnchoredPosition));
        }

        if (lastAnchoredRotation != anchor.transform.rotation)
        {
            Debug.Log("Rotation Changed: " + Quaternion.Angle(anchor.transform.rotation, lastAnchoredRotation));
        }
    }
}
