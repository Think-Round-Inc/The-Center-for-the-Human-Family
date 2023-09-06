using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public sealed class HotspotController : MonoBehaviour
{
    [SerializeField] GameObject viewer;
    [SerializeField] GameObject hotspotIcon;
    [SerializeField] Color hotspotVisitedColor = Color.gray;
    [SerializeField] float maxDistanceFromHotspot = 3f;
    [SerializeField] float visibilityThreshold = 0.5f;
    [SerializeField] TMP_Text infoText;
    [SerializeField] UnityEvent<GameObject> onNearHotspot;
    [SerializeField] Camera mainCamera;
    private List<HotSpot> hotSpots;
    GameObject closestHotspot;

    private void Start()
    {
        hotSpots = FindObjectsOfType<HotSpot>().ToList();
        if (mainCamera == null)
            mainCamera = Camera.main;
        HideHotspotIcon();
    }

    public void SetPaintingNameAndInfoToInfoText(GameObject screen)
    {
        if (screen.TryGetComponent(out PaintingData data))
            infoText.text = $"{data.paintingData.paintingName}\n{data.paintingData.extraPaintingInfo}";
    }

    public void ClearInfoPanelText() => infoText.text = string.Empty;

    public void HotSpotClickedEventTriggered()
    {
        if (closestHotspot.TryGetComponent(out HotSpot hotSpot))
            hotSpot.onClickedHotspot?.Invoke(closestHotspot);
    }

    private void Update()
    {
        float closestDistance = float.MaxValue;
        closestHotspot = null;

        Vector3 cameraForward = mainCamera.transform.forward;

        for (int i = 0; i < hotSpots.Count; i++)
        {
            Vector3 hotspotPoint = hotSpots[i].transform.position + hotSpots[i].hotspotIconOffset;
            
            Vector3 cameraToHotspot = hotspotPoint - mainCamera.transform.position;
            cameraToHotspot.Normalize();
            float dotProduct = Vector3.Dot(cameraForward, cameraToHotspot);
            float distance = Vector3.Distance(new Vector3(hotspotPoint.x, viewer.transform.position.y, hotspotPoint.z), viewer.transform.position);
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

    public void SetHotspotAsSeen()
    {
        if (closestHotspot != null)
        {
            if (closestHotspot.TryGetComponent(out HotSpot hotspot))
                hotspot.hasSeenBefore = true;
        }
    }

    public void MoveIconToHotspot(GameObject targetHotspot)
    {
        if (hotspotIcon == null || targetHotspot == null) return;

        if (targetHotspot.TryGetComponent(out HotSpot hotSpot))
        {
            Image hotspotIconImage = hotspotIcon.GetComponent<Image>();
            
            if (hotSpot.hasSeenBefore && hotspotIconImage != null)
                hotspotIconImage.color = hotspotVisitedColor;
            else
                hotspotIconImage.color = Color.white;

            Vector3 hotspotScreenPos = mainCamera.WorldToScreenPoint(targetHotspot.transform.position + hotSpot.hotspotIconOffset);
            hotspotIcon.transform.position = hotspotScreenPos;
            hotspotIcon.SetActive(true);
        }
    }

    public void PrintMessage(string message) => print($"{message}");
}
