using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System;

public class ScanStart : MonoBehaviour
{
  public GameObject FitToScanOverlay;

  public ImageController controller;

  private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

  public Camera firstPersonCamera;
    void Start()
  {
    Console.WriteLine("Hello here we start");
  }

  // Update is called once per frame
  void Update()
  {
    // Get updated augmented images for this frame.
    Session.GetTrackables<AugmentedImage>(
        m_TempAugmentedImages, TrackableQueryFilter.Updated);

    this.controller = GetComponent<ImageController>();

    FitToScanOverlay.SetActive(false);
    foreach (var image in m_TempAugmentedImages)
    {
    Debug.Log("Wohoo we have a picture found!");
        
    // move camera in front of picture
    this.firstPersonCamera.transform.position = image.CenterPose.position;
    this.firstPersonCamera.transform.rotation = new Quaternion(0, 270f, 0, 0); // TODO don't know if this is the way

    this.controller.updateAnchor(image);
    // this.DeactivateSelf();
    // todo probably have to enable this to set anchor always to keep anchor in sight
    return;
    }
  }

  void DeactivateSelf()
  {
    this.gameObject.SetActive(false);
  }
}
