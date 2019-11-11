using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

/*
 * Set ups the scene.
 */
public class SceneController : MonoBehaviour
{
    // poster in the virtual world
    public GameObject poster;

    // holds all information points at stations
    public List<GameObject> informationPoints;

    // holds all arrows
    public List<GameObject> arrows;

    // holds all objects of AMIs world, when anchored are set as children from poster to be aligned correctly
    public GameObject AMIsObjects;

    private void Awake()
    {
        this.SetupScene();
    }

    // Update is called once per frame.
    void Update()
    {
        // never let screen sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    
    // shows the objects of AMIs world
    // Should be called after successfully onboarding and
    // synchronization with real world.
    public void EnableAMIsWorld()
    {
        // show AMIs world
        this.AMIsObjects.SetActive(true);

        // show information points
        this.SetActiveInfoPoints(true);
    }

    // Hide all objects from scene.
    // should be called at startup
    private void SetupScene()
    {
        foreach(var arrow in this.arrows) {
            arrow.SetActive(false);
        }
        this.SetActiveInfoPoints(false);
        this.poster.SetActive(false);
        this.AMIsObjects.SetActive(false);

        // children of AMIs world become child of poster so they follow anchor
        Transform newParent = this.poster.transform;
        Transform oldParent = this.AMIsObjects.transform;
        while (oldParent.childCount > 0)
        {
            oldParent.GetChild(oldParent.childCount - 1).SetParent(newParent, true);
        }
    }

    // Activate/Deactivate all Infopoints.
    private void SetActiveInfoPoints(bool isActive)
    {
        foreach(var point in this.informationPoints)
        {
            point.SetActive(isActive);
        }
    }
}
