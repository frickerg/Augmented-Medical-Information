using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/**
 * Trigger attached to every information point.
 * Audio can be played and paused when icon is touched. Each audio sign has to have an individual tag.
 * Audio end is detected by coroutine timer.
 */
public class TouchAudioTrigger : MonoBehaviour
{
    // camera of the scene, needed to locate touch
    public Camera mainCamera;

    // audio source where audio clip ist stored
    private AudioSource audioSource;

    // timer to detect when finished listening audio
    private float audioListenedTimer;

    // how long it takes to fade in arrows
    private float FADE_DURATION = 4f;

    // icon to press
    public GameObject playIcon;

    // play, pause and resume material to switch when touched
    public Material playMat;
    public Material pauseMat;
    public Material resumeMat;

    // the renderer of this object
    private Renderer iconRenderer;

    // arrows that we want to enable at the end of audio
    public List<GameObject> arrows;

    // makes sure that only one sound is played
    public SoundController soundController;

    // holds state of audio: Play, Paused, Stop
    private string audioState = "Stop";

    // holds info signs of arrows
    public List<GameObject> infoSigns;

    // initialize renderer and audio source from object
    // hide the arrows with Fadeable.cs to be able to fade them in
    private void Start()
    {
        this.audioListenedTimer = 0;
        this.iconRenderer = this.playIcon.GetComponent<Renderer>();
        this.audioSource = this.playIcon.GetComponent<AudioSource>();

        foreach (GameObject arrow in arrows)
        {
            Fadeable darthFader = arrow.GetComponent<Fadeable>();
            darthFader.Hide();
        }
    }

    // Update is called once per frame on every object with that script
    void Update()
    {
        //always check for touchcount first, before checking array
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(raycast, out RaycastHit raycastHit))
            {
                // raycast collider must be same tag as unique object collider!
                // CAUTION: one time when I added new tag in editor it didn't work until I restarted unity...
                if (raycastHit.collider.CompareTag(this.playIcon.tag))
                {
                    if (this.audioSource.clip != null && !this.audioSource.isPlaying)
                    {
                        // cloth with pause material
                        this.iconRenderer.material = this.pauseMat;

                        // let sound controller play sound
                        soundController.PlayNewSound(audioSource);

                        // is there an audio source and is not playing
                        audioState = "Play";

                        // start timer
                        StartCoroutine("IncreaseTimer");
                    }
                    else if (this.audioSource.clip != null && this.audioSource.isPlaying) 
                    {
                        // cloth with play material
                        this.iconRenderer.material = this.resumeMat;

                        // let sound controller pause sound
                        soundController.PauseSound();

                        // is there an audio source and is playing
                        audioState = "Pause";

                        // stopp timer
                        StopCoroutine("IncreaseTimer");
                    }
                }
            }
        }

        if (audioListenedTimer >= audioSource.clip.length && audioState == "Play") {
            // only  audio has stopped playing, fade in the correspondig arrows

            // inform sound controller
            soundController.StopSound();

            Debug.Log("Audio stopped for: "+ playIcon.tag);
            ResetAudio();
            fadeInArrows();
        } else if (audioListenedTimer > 0 && !audioSource.isPlaying && 
            soundController.currentlyPlayingAudio != audioSource && audioState == "Play")
        {
            // audio was playing, and another started playing
            ResetAudio();
        }
    }

    // Resets timer and sets play material.
    private void ResetAudio()
    {
        audioState = "Stop";
        audioListenedTimer = 0;
        StopCoroutine("IncreaseTimer");
        iconRenderer.material = playMat;
    }

    // fades in selected arrows and info icons
    private void fadeInArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            Fadeable darthFader = arrow.GetComponent<Fadeable>();
            darthFader.FadeIn(FADE_DURATION);
        }
        foreach (GameObject info in infoSigns)
        {
            info.SetActive(true);
        }
    }

    //Coroutine that increases audioListenedTimer every second
    // should be stoped when audio was paused
    IEnumerator IncreaseTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            audioListenedTimer++;
        }

    }
}
