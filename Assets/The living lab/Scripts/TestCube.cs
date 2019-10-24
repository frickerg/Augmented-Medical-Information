using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetPosition(Pose pose)
  {
    transform.position = new Vector3(pose.position.x, pose.position.y, pose.position.z);
  }

}
