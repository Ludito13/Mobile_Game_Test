using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CloseMenu : MonoBehaviour, IPointerClickHandler, IScreen
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

    public void OnPointerClick(PointerEventData eventData)
    {
        ScreenManager.instance.Pop();
    }


}
