using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obscured : MonoBehaviour
{
    // Source: https://answers.unity.com/questions/316064/can-i-obscure-an-object-using-an-invisible-object.html
    // This script hides the component when behind an object with invisible material (use InvisibleMask.shader)
    // Should be attached to every component (can also be parent of components) that is child from poster
    // Warning: don't attach script to wall components
    void Start()
    {
        // get all renderers in this object and its children:
        var renders = GetComponentsInChildren<Renderer>();
        foreach (var render in renders)
        {
            render.material.renderQueue = 3002; // set their renderQueue
        }
    }
}
