using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using System;

public class ScanStart : MonoBehaviour
{
  public GameObject FitToScanOverlay;

  public SceneController controller;

  private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();
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

    if (this.m_TempAugmentedImages.Count > 0)
    {
      FitToScanOverlay.SetActive(false);
      foreach (var image in m_TempAugmentedImages)
      {
        Console.WriteLine("Wohoo we have a picture found!");
        this.controller.StartPosition(image);
        this.DeactivateSelf();
        return;
      }
    }
    else
    {
      FitToScanOverlay.SetActive(true);
    }
  }

  void DeactivateSelf()
  {
    this.gameObject.SetActive(false);
  }
}
