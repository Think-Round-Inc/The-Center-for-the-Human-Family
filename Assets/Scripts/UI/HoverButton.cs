using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    public UnityEvent onButtonClicked;
    public UnityEvent onPointerEnter;
    public UnityEvent onPointerExit;

    public void OnPointerDown(PointerEventData eventData) => onButtonClicked?.Invoke();

    public void OnPointerEnter(PointerEventData eventData) => onPointerEnter?.Invoke();

    public void OnPointerExit(PointerEventData eventData) => onPointerExit?.Invoke();
}
