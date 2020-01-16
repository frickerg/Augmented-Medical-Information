using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// The Animator Component of the GameObject
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// The Canvas Group Component of the GameObject
    /// </summary>
    private CanvasGroup _canvasGroup;

    /// <summary>
    /// Storing a boolean used for the Animation
    /// </summary>
    public bool IsOpen
    {
        get { return _animator.GetBool("IsOpen"); }
        set { _animator.SetBool("IsOpen", value); }
    }

    /// <summary>
    /// Called before all Start-Methods
    /// </summary>
    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponent<CanvasGroup>();

        var rect = GetComponent<RectTransform>();
        var rectparent = GetComponentInParent<RectTransform>();

        //works only horizontally
        rect.offsetMin = new Vector2(0, 0);
        rect.offsetMax = new Vector2(0, 0);

       
    }

    public void Update()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Menu_SlideIn"))
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
             
        }
        else
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
        }
    }

    //TODO: Fix this method so that the GUI-Panels are set to the correct position at start. (center of parent, anchors to stretch/stretch)
    /// <summary>
    /// Workaround for setting the Anchor-Presets in the Rect-Transform-Tool
    /// </summary>
    /// <param name="_mRect"> Rect Transform of the Component</param>
    /// <param name="_parent"> Rect Transform of the Parent Component </param>
    public void SetAndStretchToParentSize(RectTransform _mRect, RectTransform _parent)
    {
       _mRect.anchoredPosition = _parent.position;
      //_mRect.anchorMin = new Vector2(1, 0);
      // _mRect.anchorMax = new Vector2(0, 1);
      // _mRect.pivot = new Vector2(0.5f, 0.5f);
       _mRect.sizeDelta = _parent.rect.size;
       _mRect.transform.SetParent(_parent);
    }
}
