using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BirdViewController : MonoBehaviour
{
    public GameObject plane;
    public PlayerController playerController;
    public ToggleManager toggleManager;
    public float zoomSpeed;
    public float dragSpeed;

    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;
    public float zMax;
    public float zMin;

    private Vector3 dragOrigin;
    private bool dragging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        transform.position = keepInBound( new Vector3
            (transform.position.x, transform.position.y - zoom * zoomSpeed, transform.position.z));


        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click!");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit the plane
                Debug.Log(hit.collider.gameObject);
               // if (hit.collider.gameObject == plane)
                {
                    // Get the position of the intersection point
                    Vector3 position = hit.point;

                    // Do something with the position
                    Debug.Log("Clicked on position: " + position);
                    playerController.setPosition(position);
                    toggleManager.ToggleCameraMode();
                }
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Drag begin");
            dragOrigin = Input.mousePosition;
            dragging = true;
        }

        if (dragging)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            Debug.Log(pos);
            Vector3 move = new Vector3(- pos.y * dragSpeed, 0, pos.x * dragSpeed);
            transform.position = keepInBound(transform.position + move);
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(2))
        {
            Debug.Log("Drag end");
            dragging = false;
        }
    }

    private Vector3 keepInBound(Vector3 vector3)
    {
        float x = vector3.x;
        float y = vector3.y;
        float z = vector3.z;
        x = Math.Max(xMin,x);
        x = Math.Min(xMax,x);
        y = Math.Max(yMin, y);
        y = Math.Min(yMax, y);
        z = Math.Max(zMin, z);
        z = Math.Min(zMax, z);
        return new Vector3(x, y, z);
    }
}
