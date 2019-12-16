using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OverlayManager : MonoBehaviour
{
    /// <summary>
    /// The Canvas Group Component of the GameObject
    /// </summary>
    private CanvasGroup _canvasGroup;

    // holds information overlay
    public GameObject InformationflowOverlay;

    // holds game object where text is displayed
    public GameObject textObject;

    // holds text on overlay
    private TextMeshPro infoText;

    // true when overlay is shown
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

    // assign text element from component
    private void Start()
    {
        infoText = textObject.GetComponent<TextMeshPro>();
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

    // set text of overlay
    public void SetText(string text)
    {
        infoText.SetText(text);
    }
}
