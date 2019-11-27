using System.Collections;
using UnityEngine;

// Fade functionality for a 3D object with mesh renderer.
// Call FadeIn() or FadeOut() to trigger effect with desired fade duration in seconds.
// To call FadeIn() the object should be made transparent first by calling Hide().
// source for basic idea of fade: https://www.youtube.com/watch?v=oNz4I0RfsEg
// calculation for duration by josh :)
public class Fadeable : MonoBehaviour
{
    // mesh renderer of object
    private MeshRenderer renderer;

    // desired fade duration
    private float fadeDurationInSeconds;

    // set the mesh renderer of object
    void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Hide the object to do fadein later
    public void Hide()
    {
        Color c = renderer.material.color;
        c.a = 0f;
        renderer.material.color = c;
    }

    // Fades in the object in x seconds.
    public void FadeIn(float durationInSeconds)
    {
        this.fadeDurationInSeconds = durationInSeconds;
        StartCoroutine("DoTheFadeIn");
    }

    // Fades in the object
    IEnumerator DoTheFadeIn()
    {
        // 1 is opacity 100%
        float timeout = 0.05f;
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = fadeAmount; f <= 1; f += fadeAmount)
        {
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
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
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return new WaitForSeconds(timeout);
        }
    }
}
