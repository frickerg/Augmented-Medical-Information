using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    // holds the invisible material
    public Material invisibleMat;

    // set all walls to invisible material
    void Start()
    {
        // get all renderers in this object and its children:
        var renders = GetComponentsInChildren<Renderer>();
        foreach (var render in renders)
        {
            render.material = invisibleMat;
        }
    }
}
