using UnityEngine;
using System.Collections;

public class TouchAudioTrigger : MonoBehaviour
{
    // camera of the scene, needed to locate touch
    public Camera mainCamera;

    // audio source to attach audio clip
    public AudioSource audioSource;

    // icon to press
    public GameObject playIcon;

    // play and pause textures to switch when touched
    public Material playMat;
    public Material pauseMat;

    // the renderer of this object
    private Renderer iconRenderer;

    // initialize renderer
    private void Start()
    {
        this.iconRenderer = GetComponent<Renderer>();
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
                // check for tag of objects collider
                // CAUTION 1: objects to touch have to have a collider
                // CAUTION 2: when I set the tag in editor it didn't work until I restarted unity...
                if (raycastHit.collider.CompareTag("PlayIcon"))
                {
                    if (!audioSource.isPlaying)
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
}
