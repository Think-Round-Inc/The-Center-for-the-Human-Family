using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HotspotIcon : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    public static event Action HotSotClicked;
    public UnityEvent onPointerEnter;
    public UnityEvent onPointerExit;

    public void OnPointerDown(PointerEventData eventData) => HotSotClicked?.Invoke();

    public void OnPointerEnter(PointerEventData eventData) => onPointerEnter?.Invoke();

    public void OnPointerExit(PointerEventData eventData) => onPointerExit?.Invoke();
}