using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class TriggerArrow : MonoBehaviour
{
    // arrow that we want to enable
    public GameObject arrow;

    private void OnTriggerEnter(Collider collider)
    {
        this.arrow.SetActive(true);
    }
}
