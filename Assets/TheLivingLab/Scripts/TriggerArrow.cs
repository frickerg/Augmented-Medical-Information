using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class TriggerArrow : MonoBehaviour
{
    // arrows that we want to enable
    public List<GameObject> arrows;

    // Is called when another object with a collider component touches this object.
    // CAUTION: every object has per default a collider
    // A trigger is only called when at least one of the colliding objects has a) a rigidbody component
    // and b) when for one of the objects triggering enabled is
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
