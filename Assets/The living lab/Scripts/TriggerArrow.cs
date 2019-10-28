using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArrow : MonoBehaviour
{
    // arrow that we want to enable
    public GameObject arrow;

    private void OnTriggerEnter(Collision collision)
    {
        this.arrow.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
