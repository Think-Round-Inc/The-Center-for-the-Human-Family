using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class HotspotController : MonoBehaviour
{
    [SerializeField] GameObject viewer;
    [SerializeField] GameObject hotspotIcon;
    [SerializeField] float maxDistanceFromHotspot = 3f;
    [SerializeField] float visibilityThreshold = 0.5f; // Adjust this value as needed
    [SerializeField] UnityEvent<GameObject> onNearHotspot;
    [SerializeField] Camera mainCamera;
    private List<HotSpot> hotSpots;

    private void Start()
    {
        hotSpots = FindObjectsOfType<HotSpot>().ToList();
        if (mainCamera == null)
            mainCamera = Camera.main;
        HideHotspotIcon();
    }

    private void Update()
    {
        float closestDistance = float.MaxValue;
        GameObject closestHotspot = null;

        Vector3 cameraForward = mainCamera.transform.forward;

        for (int i = 0; i < hotSpots.Count; i++)
        {
            Vector3 hotspotPoint = hotSpots[i].transform.position;
            hotspotPoint.y = viewer.transform.position.y;

            Vector3 cameraToHotspot = hotspotPoint - mainCamera.transform.position;
            cameraToHotspot.Normalize();

            float dotProduct = Vector3.Dot(cameraForward, cameraToHotspot);

            float distance = Vector3.Distance(hotspotPoint, viewer.transform.position);

            if (dotProduct > visibilityThreshold && distance < maxDistanceFromHotspot && distance < closestDistance)
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

        if (targetHotspot.TryGetComponent(out HotSpot hotSpot))
        {
            Vector3 hotspotScreenPos = mainCamera.WorldToScreenPoint(targetHotspot.transform.position + hotSpot.hotspotIconOffset);
            hotspotIcon.transform.position = hotspotScreenPos;
            hotspotIcon.SetActive(true);
        }
    }

    public void PrintMessage(string message) => print($"{message}");
}
