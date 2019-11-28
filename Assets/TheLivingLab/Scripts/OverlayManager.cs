using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    /// <summary>
    /// The Canvas Group Component of the GameObject
    /// </summary>
    private CanvasGroup _canvasGroup;

    public GameObject InformationflowOverlay;

    private bool isOpen = false;

    public void Awake()
    {
        
        _canvasGroup = GetComponent<CanvasGroup>();
        var rect = GetComponent<RectTransform>();
        

        //works only horizontally
        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);
        InformationflowOverlay.SetActive(false);

    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;

        }
        else
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
        }
    }


public void showOverlay()
    {
        fetchContentfromFile();
        InformationflowOverlay.SetActive(true);
        isOpen = true;

    }

    public void hideOverlay()
    {
        fetchContentfromFile();
        InformationflowOverlay.SetActive(false);
        isOpen = false;

    }

    public void fetchContentfromFile()
    {
        // to be implemented
    }
}
