using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HotspotIcon : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    public UnityEvent<PointerEventData> onHotSpotClicked;
    public UnityEvent<PointerEventData> onPointerEnter;
    public UnityEvent<PointerEventData> onPointerExit;

    public void OnPointerDown(PointerEventData eventData) => onHotSpotClicked?.Invoke(eventData);

    public void OnPointerEnter(PointerEventData eventData) => onPointerEnter?.Invoke(eventData);

    public void OnPointerExit(PointerEventData eventData) => onPointerExit?.Invoke(eventData);
}
