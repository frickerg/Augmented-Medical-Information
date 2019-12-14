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

    // holds object with information points as children
    public GameObject informationPoints;

    // holds object with arrows as children
    public GameObject arrows;

    // holds all objects of AMIs world, are set as children of poster in SetupScene()
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
        // TODO show something else than just poster to show that scanned successful
        this.poster.SetActive(true);
    }

    // Hide all objects from scene.
    // should be called at startup
    private void SetupScene()
    {
        // no need anymore because we use Fadeable.cs
        // this.SetActiveArrows(false);
        this.poster.SetActive(false);

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
        // loop over children of infopoint holder
        Transform[] allPoints = this.informationPoints.GetComponentsInChildren<Transform>();
        foreach (Transform infoPoint in allPoints)
        {
            infoPoint.gameObject.SetActive(isActive);
        }
    }

    // Activate/Deactivate all arrows.
    private void SetActiveArrows(bool isActive)
    {
        // loop over children of arrow holder
        Transform[] allArrows = this.arrows.GetComponentsInChildren<Transform>();
        foreach (Transform arrow in allArrows)
        {
            arrow.gameObject.SetActive(isActive);
        }
    }
}
