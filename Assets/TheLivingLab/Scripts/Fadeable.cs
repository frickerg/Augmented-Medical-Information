using System.Collections;
using UnityEngine;

// Fade functionality for a 3D object with mesh renderer.
// Call FadeIn() or FadeOut() to trigger effect with desired fade duration in seconds.
// To call FadeIn() the object should be made transparent first by calling Hide().
// IMPORTANT: make sure that material of gameobject is same as fadingMaterial in Assets material folder.
// source for basic idea of fade: https://www.youtube.com/watch?v=oNz4I0RfsEg
// calculation for duration by josh :)
public class Fadeable : MonoBehaviour
{
    // mesh renderer of object
    private MeshRenderer meshRender;

    // desired fade duration
    private float fadeDurationInSeconds;

    // set the mesh renderer of object
    void Awake()
    {
        meshRender = GetComponent<MeshRenderer>();
    }

    // Hide the object to do fadein later
    public void Hide()
    {
        Color c = meshRender.material.color;
        c.a = 0;
        meshRender.material.color = c;
    }

    // Fades in the object in x seconds.
    // only fades when object was hidden on alpha channel
    public void FadeIn(float durationInSeconds)
    {
        Color c = meshRender.material.color;
        if (c.a == 0)
        {
            fadeDurationInSeconds = durationInSeconds;
            StartCoroutine("DoTheFadeIn");
        }
    }

    // Fades in the object
    IEnumerator DoTheFadeIn()
    {
        // 1 is opacity 100%
        float timeout = 0.05f;
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = fadeAmount; f <= 1; f += fadeAmount)
        {
            Color c = meshRender.material.color;
            c.a = f;
            meshRender.material.color = c;
            yield return new WaitForSeconds(timeout);
        }
    }

    // Fades in the object in x seconds.
    public void FadeOut(float durationInSeconds)
    {
        this.fadeDurationInSeconds = durationInSeconds;
        StartCoroutine("DoTheFadeOut");
    }

    // Fades out the object
    IEnumerator DoTheFadeOut()
    {
        // 0 is opacity 0%
        float timeout = 0.05f;
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);
        
        for (float f = 1; f > 0; f -= fadeAmount)
        {
            Color c = meshRender.material.color;
            c.a = f;
            meshRender.material.color = c;
            yield return new WaitForSeconds(timeout);
        }
    }
}
