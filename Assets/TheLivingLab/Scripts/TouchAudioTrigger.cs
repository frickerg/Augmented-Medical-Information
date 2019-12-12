using UnityEngine;
using System.Collections.Generic;
using GoogleARCore;

public class TouchAudioTrigger : MonoBehaviour
{
    // camera of the scene, needed to locate touch
    public Camera mainCamera;

    // audio source where audio clip ist stored
    private AudioSource audioSource;

    // icon to press
    public GameObject playIcon;

    // play and pause textures to switch when touched
    public Material playMat;
    public Material pauseMat;
    public Material resumeMat;

    // flag to determine if the audio has been stopped before finish
    public bool canResume;

    // the renderer of this object
    private Renderer iconRenderer;

    // arrows that we want to enable at the end of audio
    public List<GameObject> arrows;

    // initialize renderer and audio source from object
    private void Start()
    {
        this.canResume = false;
        this.iconRenderer = this.playIcon.GetComponent<Renderer>();
        this.audioSource = this.playIcon.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(raycast, out RaycastHit raycastHit))
            {
                // raycast collider must be same tag as unique object collider!
                // CAUTION: when I set the tag in editor it didn't work until I restarted unity...
                if (raycastHit.collider.CompareTag(this.playIcon.tag))
                {
                    if (this.audioSource.clip != null && !this.audioSource.isPlaying)
                    {
                        // play the video and cloth with pause material
                        this.audioSource.Play();
                        this.iconRenderer.material = this.pauseMat;

                        // increase play count
                        this.canResume = false;
                    }
                    else
                    {
                        // pause the video and cloth with play material
                        this.audioSource.Pause();
                        this.iconRenderer.material = this.resumeMat;

                        // audio can be resumed
                        this.canResume = true;
                    }
                }
            }
        }
        // once audio has stopped playing, fade in the correspondig arrows
        if (!this.audioSource.isPlaying && !this.canResume)
        {
            this.iconRenderer.material = this.playMat;
            fadeInArrows();
        }
    }

    // fades in selected arrows
    private void fadeInArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            Fadeable darthFader = arrow.GetComponent<Fadeable>();
            darthFader.FadeIn(5f);
        }
    }
}
