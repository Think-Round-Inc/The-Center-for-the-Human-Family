using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public sealed class HotSpot : MonoBehaviour
{
    public bool hasSeenBefore;
    public Vector3 hotspotIconOffset;
    public UnityEvent onClickedViewButton;
}
