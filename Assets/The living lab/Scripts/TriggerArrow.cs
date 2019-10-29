using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class TriggerArrow : MonoBehaviour
{
    // holds the screen controller object
    public GameObject sceneController;

    // arrow that we want to enable
    public List<GameObject> arrows;

    // holds the scene controller script
    private SceneController controller;

    void Awake()
    {
        this.controller = this.sceneController.GetComponent<SceneController>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "MainCamera") 
        {
            // only trigger when collided with camera
            foreach (var arrow in arrows)
            {
                arrow.SetActive(true);
            }
        }   
    }
}
