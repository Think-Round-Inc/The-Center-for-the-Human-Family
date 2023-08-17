using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public sealed class HotSpot : MonoBehaviour
{
    [TextArea(0, 10)] public string hotspotInfo;
    public Vector3 hotspotIconOffset;
}
