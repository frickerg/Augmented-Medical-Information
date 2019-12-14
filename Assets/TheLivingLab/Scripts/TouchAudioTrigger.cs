using UnityEngine;
using System.Collections.Generic;
using GoogleARCore;
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

    // holds tag of last touched audio sign
    private string lastTouchedTag = "";

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
                    // TODO check if other is playing

                    //only the touched object should be handled
                    this.lastTouchedTag = this.playIcon.tag;

                    if (this.audioSource.clip != null && !this.audioSource.isPlaying)
                    {
                        // is there an audio source and is not playing

                        // play the audio and cloth with pause material
                        this.audioSource.Play();
                        this.iconRenderer.material = this.pauseMat;

                        // start timer
                        StartCoroutine("IncreaseTimer");
                    }
                    else if (this.audioSource.clip != null && this.audioSource.isPlaying) 
                    {
                        // is there an audio source and is playing

                        // pause the video and cloth with play material
                        this.audioSource.Pause();
                        this.iconRenderer.material = this.resumeMat;

                        // stopp timer
                        StopCoroutine("IncreaseTimer");
                    }
                }
            }
        }

        if (lastTouchedTag == playIcon.tag && audioListenedTimer >= audioSource.clip.length) {
            // only  audio has stopped playing, fade in the correspondig arrows
            Debug.Log("Audio stopped for: "+ playIcon.tag);
            audioListenedTimer = 0;
            StopCoroutine("IncreaseTimer");
            iconRenderer.material = playMat;
            fadeInArrows();
        }
    }

    // fades in selected arrows
    private void fadeInArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            Fadeable darthFader = arrow.GetComponent<Fadeable>();
            darthFader.FadeIn(FADE_DURATION);
        }
    }

    //Coroutine that increases audilListenedTimer every second
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
