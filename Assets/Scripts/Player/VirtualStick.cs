using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class VirtualStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    protected Image stickGraphic;
    protected Vector2 startPosition;
    protected Vector2 currentPosition;
    protected Vector2 normalizedPosition;

    [SerializeField]
    protected float maxStickDelta;
    protected float squaredMaxDelta;

    [SerializeField]
    protected UnityEvent<Vector2> onMovement;
    [SerializeField]
    protected UnityEvent onMovementEnd;

    public Vector2 value
    {
        get { return normalizedPosition; }
    }

    virtual protected void Start()
    {
        startPosition = stickGraphic.transform.position;
        squaredMaxDelta = maxStickDelta * maxStickDelta;
        stickGraphic.enabled = false;
    }

    virtual public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    virtual public void OnDrag(PointerEventData eventData)
    {
        currentPosition += eventData.delta;
        if (currentPosition.sqrMagnitude > squaredMaxDelta)
            currentPosition = currentPosition.normalized * maxStickDelta;

        normalizedPosition = currentPosition / maxStickDelta;
        stickGraphic.transform.position = startPosition + currentPosition;
        
        if (normalizedPosition.sqrMagnitude > 0)
            onMovement?.Invoke(normalizedPosition);
    }

    virtual public void OnEndDrag(PointerEventData eventData)
    {
        stickGraphic.transform.position = startPosition;
        currentPosition.x = 0f;
        currentPosition.y = 0f;
        normalizedPosition.x = 0f;
        normalizedPosition.y = 0f;

        onMovementEnd?.Invoke();
    }


}
