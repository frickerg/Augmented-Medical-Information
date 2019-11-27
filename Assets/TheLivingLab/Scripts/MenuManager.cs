using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Menu CurrentMenu;

    public void Start()
    {
        //ShowMenu(CurrentMenu);
        CurrentMenu.IsOpen = true;
    }

    /// <summary>
    /// Activate Animation of Menu by setting the IsOpen-Bool
    /// </summary>
    /// <param name="menu"></param>
    public void ShowMenu(Menu menu)
    {
        if (CurrentMenu != null)
            CurrentMenu.IsOpen = false;

        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;
    }
}
