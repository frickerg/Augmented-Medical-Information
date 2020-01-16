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

    // holds game object where title is displayed
    public GameObject titleObject;

    // true when overlay is shown
    private bool isOpen = false;

    public void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        var rect = GetComponent<RectTransform>();
        
        //works only horizontally
        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);

        // make sure overlay is active, but hidden
        InformationflowOverlay.SetActive(true);
        hideOverlay();
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

    // show overlay with alpha channel
public void showOverlay()
    {
        fetchContentfromFile();
        _canvasGroup.alpha = 1;
        isOpen = true;
    }

    // hide overlay with alpha channel
    public void hideOverlay()
    {
        fetchContentfromFile();
        _canvasGroup.alpha = 0;
        isOpen = false;
    }

    public void fetchContentfromFile()
    {
        // to be implemented
    }

    // set text of overlay
    public void SetText(string text)
    {
        TextMeshProUGUI infoText = textObject.GetComponent<TextMeshProUGUI>();
        infoText.SetText(text);
    }

    // set title of overlay
    public void SetTitle(string text)
    {
        TextMeshProUGUI infoTitle = titleObject.GetComponent<TextMeshProUGUI>();
        infoTitle.SetText(text);
    }
}
