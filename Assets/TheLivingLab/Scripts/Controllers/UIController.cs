using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public Animator Volume_Slider;

    public void ToggleVolume()
    {
        bool state = Volume_Slider.GetBool("isHidden");
        Volume_Slider.SetBool("isHidden", !state);
       
    }

    
}
