using UnityEngine;
using UnityEngine.Events;

public class SwitchEvent : MonoBehaviour
{
    [SerializeField] bool currentState;
    [SerializeField] UnityEvent onSwitchedOn;
    [SerializeField] UnityEvent onSwitchedOff;


    public void Switch()
    {
        if (currentState)
        {
            currentState = false;
            onSwitchedOff.Invoke();
        }
        else
        {
            currentState = true;
            onSwitchedOn.Invoke();
        }
    }
}
