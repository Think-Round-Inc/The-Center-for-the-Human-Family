using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ZoomCanvas : MonoBehaviour
{
    public CanvasRenderer zoomCanvasRenderer;
    [SerializeField] UnityEvent onCanvasOpened;
    [SerializeField] UnityEvent onCanvasClosed;

    private void Start()
    {
        zoomCanvasRenderer.gameObject.SetActive(false);
    }

    public void OpenCanvas()
    {
        zoomCanvasRenderer.gameObject.SetActive(true);
        onCanvasOpened?.Invoke();
        print("Canvas Opened");
    }

    public void SwitchCanvasVisibility()
    {
        if (zoomCanvasRenderer.gameObject.activeSelf)
            CloseCanvas();
        else
            OpenCanvas();
    }

    public void CloseCanvas()
    {
        zoomCanvasRenderer.gameObject.SetActive(false);
        onCanvasClosed?.Invoke();
        print("Canvas Closed");
    }
}
