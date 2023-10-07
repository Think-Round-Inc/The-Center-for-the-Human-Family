using UnityEngine;
using UnityEngine.Events;

public class ExitController : MonoBehaviour
{
    [SerializeField] KeyCode exitKey = KeyCode.Escape;
    [SerializeField] UnityEvent onExitKeyPressed;

    private void Update()
    {
        if (Input.GetKeyDown(exitKey))
            onExitKeyPressed?.Invoke();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
