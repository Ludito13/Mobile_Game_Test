using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
//Controles de combate del player
public class CombatVirtualStick : VirtualControl, IPointerDownHandler, IPointerUpHandler
{
    protected float dragStartTime;
    [SerializeField]
    protected float maxDragTimetoDash;
    protected Vector2 accumulatedDeltaThreshold;
    protected float accumulatedDeltaMagnitude;
    [SerializeField]
    protected UnityEvent<Vector2> onDashEvent;
    [SerializeField]
    protected UnityEvent onAttackEvent;
    protected bool attackTimeout;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        attackTimeout = true;
        dragStartTime = Time.timeSinceLevelLoad;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        if (Time.timeSinceLevelLoad - dragStartTime <= maxDragTimetoDash)
            onDashEvent?.Invoke(normalizedPosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!attackTimeout)
            onAttackEvent?.Invoke();
        else
            attackTimeout = false;
    }
}
