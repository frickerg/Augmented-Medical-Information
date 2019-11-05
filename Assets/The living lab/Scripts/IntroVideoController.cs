using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideoController : MonoBehaviour
{

    public GameObject playButton;
    public GameObject pauseButton;
    public VideoPlayer videoPlayer;

    // Hide pause button
    void Start()
    {
        this.pauseButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // source: https://stackoverflow.com/questions/38565746/tap-detection-on-a-gameobject-in-unity
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                // check for tag of objects collider
                // CAUTION: when I set the tag in editor it didn't work until I restarted unity...
                if (raycastHit.collider.CompareTag("VideoScreen"))
                {
                    if (!videoPlayer.isPlaying)
                    {
                        // play the video and show pause button
                        videoPlayer.Play();
                        playButton.SetActive(false);
                        pauseButton.SetActive(true);
                    } else
                    {
                        // pause the video and show play button
                        videoPlayer.Pause();
                        playButton.SetActive(true);
                        pauseButton.SetActive(false);
                    }
                }
            }
        }
    }
}
