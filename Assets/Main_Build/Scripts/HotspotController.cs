using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class HotspotController : MonoBehaviour
{
    [SerializeField] GameObject viewer;
    [SerializeField] GameObject hotspotIcon;
    [SerializeField] List<HotSpot> hotSpots;
    [SerializeField] float maxDistanceFromHotspot = 3f;
    [SerializeField] UnityEvent<GameObject> onNearHotspot;
    [SerializeField] Camera mainCamera;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        HideHotspotIcon();
    }


    private void Update()
    {
        float closestDistance = float.MaxValue;
        GameObject closestHotspot = null;

        for (int i = 0; i < hotSpots.Count; i++)
        {
            float distance = Vector3.Distance(hotSpots[i].transform.position, viewer.transform.position);

            if (distance < maxDistanceFromHotspot && distance < closestDistance)
            {
                closestDistance = distance;
                closestHotspot = hotSpots[i].gameObject;
            }
        }

        if (closestHotspot != null)
        {
            onNearHotspot.Invoke(closestHotspot);
            MoveIconToHotspot(closestHotspot);
        }
        else
        {
            HideHotspotIcon();
        }
    }

    public void HideHotspotIcon() => hotspotIcon.SetActive(false);

    public void MoveIconToHotspot(GameObject targetHotspot)
    {
        if (hotspotIcon == null || targetHotspot == null) return;

        Vector3 hotspotScreenPos = mainCamera.WorldToScreenPoint(targetHotspot.transform.position);
        hotspotIcon.transform.position = hotspotScreenPos;
        hotspotIcon.SetActive(true);
    }

    public void PrintMessage(string message) => print($"{message}");
}


