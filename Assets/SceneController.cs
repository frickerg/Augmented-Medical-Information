using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SceneController : MonoBehaviour
{
  public Camera firstPersonCamera;

  public TestCube testCube;
  private Anchor anchor;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  // sets the start position of the world
  public void StartPosition(AugmentedImage image)
  {
    Pose location = image.CenterPose;
    this.anchor = image.CreateAnchor(image.CenterPose);
    testCube.SetPosition(location);
  }
}
