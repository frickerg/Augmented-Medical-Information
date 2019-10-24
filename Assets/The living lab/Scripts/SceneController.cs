using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SceneController : MonoBehaviour
{
  public Camera firstPersonCamera;

  private Anchor anchor;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  // Set Anchor of image at the wall in AR to reference with real world
  public void StartPosition(AugmentedImage image)
  {
        // Vector represents 3D vector
        Vector3 startVector = new Vector3(0, 1.58f, -2.77f);

        // Quaternion represents the rotation 
        Quaternion startRotation = new Quaternion(0, 1.58f, 90f, 0);

        // create AR location
        Pose location = new Pose(startVector, startRotation);

        // this.anchor = image.CreateAnchor(location);
  }
}
