using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleManager : MonoBehaviour
{
    public GameObject playerController;
    public GameObject birdViewController;
    public GameObject singlePaintingViewController;
    // Start is called before the first frame update


    public bool inPlayer;
    public bool inBird;
    public bool inSinglePainting;
    void Start()
    {
        inPlayer = true;
        inBird = false;
        inSinglePainting = false;
        UpdateCamera();
    }

    void UpdateCamera()
    {
        birdViewController.SetActive(inBird);
        playerController.SetActive(inPlayer);
        singlePaintingViewController.SetActive(inSinglePainting);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Toggle");
            ToggleCameraMode();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (inPlayer)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the ray hit the plane
                    if (hit.collider.gameObject.name == "StandMesh")
                    {
                        // Get the position of the intersection point
                        Vector3 position = hit.point;

                        // Do something with the position
                        Debug.Log("press f on : " + hit.collider.gameObject.name);
                        MeshRenderer meshRenderer = hit.collider.gameObject.GetComponent<MeshRenderer>();

                        // Get the second material in the materials array
                        Material secondMaterial = meshRenderer.materials[1];
                        Debug.Log("material : " + meshRenderer.materials[1].name);
                        //toggleManager.enterSinglePaintingView();
                        enterSinglePaintingView(secondMaterial);
                    }
                }
            } else if (inSinglePainting)
            {
                inPlayer = true;
                inSinglePainting = false;
                inBird = false;
                UpdateCamera();
            }
        }
    }

    public void ToggleCameraMode()
    {
        inPlayer = !inPlayer;
        inBird = !inBird;
        UpdateCamera();
    }

    public void enterSinglePaintingView(Material material)
    {
        inSinglePainting = true;
        inPlayer = false;
        inBird = false;
        singlePaintingViewController.GetComponent<SinglePaintingViewController>().setMaterial(material);
        UpdateCamera();

    }

    public void quitSinglePaintingView()
    {
        birdViewController.SetActive(false);
        playerController.SetActive(true);
        singlePaintingViewController.SetActive(false);
    }
}
