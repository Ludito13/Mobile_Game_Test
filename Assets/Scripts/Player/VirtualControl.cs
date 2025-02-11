using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class VirtualControl : VirtualStick
{

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        stickGraphic.transform.position = eventData.position;
        startPosition = eventData.position;
        stickGraphic.enabled = true;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        stickGraphic.enabled = false;
    }
}
