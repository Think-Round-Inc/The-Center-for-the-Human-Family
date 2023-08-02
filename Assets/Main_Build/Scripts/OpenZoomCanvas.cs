using UnityEngine;
using UnityEngine.UI;

public class OpenZoomCanvas : MonoBehaviour
{
    public GameObject zoomableCanvas;

    private void Start()
    {
        zoomableCanvas.SetActive(false);
    }

    public void OpenCanvas()
    {
        zoomableCanvas.SetActive(true);
    }

    public void CloseCanvas()
    {
        zoomableCanvas.SetActive(false);
    }
}
