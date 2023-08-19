using UnityEngine;
using UnityEngine.UI;

public class OpenZoomCanvas : MonoBehaviour
{
    public CanvasRenderer zoomCanvasRenderer;
    public GameObject zoomableCanvas;

    private void Start()
    {
        zoomableCanvas.SetActive(false);
        zoomCanvasRenderer.gameObject.SetActive(false);
    }

    public void OpenCanvas()
    {
        if (zoomableCanvas.activeSelf)
        {
            CloseCanvas();
            return;
        }
        zoomCanvasRenderer.gameObject.SetActive(true);
        zoomableCanvas.SetActive(true);
    }

    public void CloseCanvas()
    {
        if (!zoomableCanvas.activeSelf)
        {
            OpenCanvas();
            return;
        }
        zoomableCanvas.SetActive(false);
        zoomCanvasRenderer.gameObject.SetActive(false);
    }
}
