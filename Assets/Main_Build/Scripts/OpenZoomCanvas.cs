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
        zoomCanvasRenderer.gameObject.SetActive(true);
        zoomableCanvas.SetActive(true);
    }

    public void CloseCanvas()
    {
        zoomableCanvas.SetActive(false);
        zoomCanvasRenderer.gameObject.SetActive(false);
    }
}
