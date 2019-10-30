using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class TriggerArrow : MonoBehaviour
{
    // arrows that we want to enable
    public List<GameObject> arrows;

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
