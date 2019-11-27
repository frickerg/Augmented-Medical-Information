using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to fade in an object.
// Call Start
public class Fadeable : MonoBehaviour
{
    private MeshRenderer renderer;

    // Set object to transparent in the beginning
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        Color c = renderer.material.color;
        c.a = 0f;
        renderer.material.color = c;
    }

    // checks for mouse click and calles fade in
    void Update()
    {
        if ( Input.GetMouseButtonDown(0))
        {
            FadeIn();
        }
    }

    // starts the fading process
    public void FadeIn()
    {
        StartCoroutine("DoTheFade");
    }

    // Fades in the object
    IEnumerator DoTheFade()
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
