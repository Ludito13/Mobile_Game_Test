using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screens : MonoBehaviour, IScreen
{
    public Screens screenNext;

    public void BTN_Close()
    {
        ScreenManager.instance.Pop();
    }

    public void BTN_Active()
    {
        if (screenNext != null)
            ScreenManager.instance.ActiveScreen(screenNext);
    }
    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Desactivate()
    {
        gameObject.SetActive(false); 
    }
}
