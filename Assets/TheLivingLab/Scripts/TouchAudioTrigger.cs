﻿using UnityEngine;
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

    // arrows that should be showed when audio is finished
    public List<GameObject> arrowsToTrigger;

    // the renderer of this object
    private Renderer iconRenderer;

    // initialize renderer and audio source from object
    private void Start()
    {
        this.iconRenderer = this.playIcon.GetComponent<Renderer>();
        this.audioSource = this.playIcon.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                // raycast collider must be same tag as unique object collider!
                // CAUTION: when I set the tag in editor it didn't work until I restarted unity...
                if (raycastHit.collider.CompareTag(this.playIcon.tag))
                {
                    if (audioSource.clip != null && !audioSource.isPlaying)
                    {
                        // play the video and cloth with pause material
                        audioSource.Play();
                        this.iconRenderer.material = this.pauseMat;
                    }
                    else
                    {
                        // pause the video and cloth with play material
                        audioSource.Pause();
                        this.iconRenderer.material = this.playMat;
                    }
                }
            }
        }

        // TODO change material to play again, when audio clip is finished
    }

    // Displays the arrows
    private void showArrows()
    {
        //TODO
    }
}