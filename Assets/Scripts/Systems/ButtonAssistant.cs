using UnityEngine.Events;
using UnityEngine;

public class ButtonAssistant : MonoBehaviour
{
    [SerializeField] UnityEvent onClickedOn;
    [SerializeField] UnityEvent onClickedOff;
    bool currentState = false;
    public void ButtonClicked()
    {
        if (currentState)
        {
            currentState = false;
            onClickedOff?.Invoke();
        }
        else
        {
            currentState = true;
            onClickedOn?.Invoke();
        }
    }
}
