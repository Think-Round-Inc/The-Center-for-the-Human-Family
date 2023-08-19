using UnityEngine;
using UnityEngine.UI;

public class ZoomAndDragCanvas : MonoBehaviour
{
    public float zoomSpeed = 1f;
    public float minZoom = 1f;
    public float maxZoom = 3f;
    public float dragSpeed = 1f;

    private RectTransform canvasRectTransform;

    private Vector3 lastMousePosition;

    private void Start()
    {
        canvasRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Zoom
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        float newScale = Mathf.Clamp(canvasRectTransform.localScale.x + zoomDelta, minZoom, maxZoom);
        canvasRectTransform.localScale = Vector3.one * newScale;

        // Drag
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            canvasRectTransform.anchoredPosition += (Vector2)deltaMouse * dragSpeed;
            lastMousePosition = Input.mousePosition;
        }
    }
}
