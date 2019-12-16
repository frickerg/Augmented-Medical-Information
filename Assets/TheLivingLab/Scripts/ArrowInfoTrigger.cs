using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Shows on touch information of the arrow as UI overlay.
 */
public class ArrowInfoTrigger : MonoBehaviour
{
    // text for arrow info comes from text file
    public TextAsset arrowInfo;

    // text for arrow title comes from text file
    public TextAsset infoTitle;

    // UI object to display information
    public OverlayManager infoManager;

    // Update is called once per frame
    void Update()
    {
        //always check for touchcount first, before checking array
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(raycast, out RaycastHit raycastHit))
            {
                // raycast collider must be same tag as unique object collider!
                if (raycastHit.collider.CompareTag(gameObject.tag))
                {
                    infoManager.SetTitle(infoTitle.text);
                    infoManager.SetText(arrowInfo.text);
                    infoManager.showOverlay();
                }
            }
        }
    }
}
