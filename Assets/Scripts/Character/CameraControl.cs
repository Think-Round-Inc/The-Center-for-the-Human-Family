using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

public class CameraControl : MonoBehaviour
{
    [Header("Configure")]
    [SerializeField] float panSpeed = 20;

    [Header("Reference")]
    [SerializeField] Transform toPan = null;

    void Update()
    {
        //PanCamera();
    }

    public void PanCamera()
    {
        if(toPan == null) { return; }
        //Pan the camera pivot
        if (Input.GetMouseButton(1))
        {
            float x = -Input.GetAxis("Mouse Y") * panSpeed;
            float y = Input.GetAxis("Mouse X") * panSpeed;

            // Rotate the camera with respect to mouse movement
            toPan.Rotate(x, y, 0f);

            x = toPan.rotation.eulerAngles.x;
            y = toPan.rotation.eulerAngles.y;
            toPan.rotation = Quaternion.Euler(x, y, 0);
        }

        //if (Input.GetMouseButton(0))
        //{
        //    float x = -Input.GetAxis("Mouse Y") * panSpeed;
        //    float y = Input.GetAxis("Mouse X") * panSpeed;
            
        //    // Rotate the camera with respect to mouse movement
        //    toPan.Rotate(x, y, 0f);

        //    x = toPan.rotation.eulerAngles.x;
        //    y = toPan.rotation.eulerAngles.y;
            
        //    toPan.rotation = Quaternion.Euler(x, y, 0);
        //}
    }

    public Transform GetPivot()
    {
        return toPan;
    }

    /// <summary>
    /// Set Camera Pan Speed
    /// </summary>
    /// <param name="newSpeed"></param>
    public void SetPanSpeed(float newSpeed)
    {
        panSpeed = newSpeed;
    }
}
