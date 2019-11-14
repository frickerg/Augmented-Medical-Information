using UnityEngine;
using System.Collections;

public class TriggerAudio : MonoBehaviour
{
    public Camera mainCamera;
    public AudioSource audioSource;
    public GameObject playIcon;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playIcon.transform.LookAt(mainCamera.transform);

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                // check for tag of objects collider
                // CAUTION: when I set the tag in editor it didn't work until I restarted unity...
                if (raycastHit.collider.CompareTag("PlayIcon"))
                {
                    if (!audioSource.isPlaying)
                    {
                        // play the video and show pause button
                        audioSource.Play();
                        //playIcon.SetActive(false);
                        //playIcon.SetActive(true);
                    }
                    else
                    {
                        // pause the video and show play button
                        audioSource.Pause();
                        //playIcon.SetActive(true);
                        //playIcon.SetActive(false);
                    }
                }
            }
        }

    }
}
