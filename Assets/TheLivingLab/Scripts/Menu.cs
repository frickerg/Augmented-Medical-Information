using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Animator _animator;
    private CanvasGroup _canvasGroup;

    public bool IsOpen
    {
        get { return _animator.GetBool("IsOpen"); }
        set { _animator.SetBool("IsOpen", value); }
    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponent<CanvasGroup>();

        var rect = GetComponent<RectTransform>();
        var rectparent = GetComponentInParent<RectTransform>();
        SetAndStretchToParentSize(rect, rectparent);
        //rect.offsetMax = rect.offsetMin = new Vector2(0, 0);
        //rect.anchoredPosition = new Vector2(0, 0);
        //rect.anchorMax = rect.anchorMin = new Vector2(0,0);

        

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

    //TODO: Fix this method so that the GUI-Panels are set to the correct position at start. 
    public void SetAndStretchToParentSize(RectTransform _mRect, RectTransform _parent)
    {
       _mRect.anchoredPosition = _parent.position;
       _mRect.anchorMin = new Vector2(1, 0);
       _mRect.anchorMax = new Vector2(0, 1);
       _mRect.pivot = new Vector2(0.5f, 0.5f);
       _mRect.sizeDelta = _parent.rect.size;
       _mRect.transform.SetParent(_parent);
    }
}
