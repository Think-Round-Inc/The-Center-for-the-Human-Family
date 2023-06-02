using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SectionNode : MonoBehaviour
{
    [SerializeField] MeshRenderer nodeFloor = null; //used to get dimensions of node for path positioning

    // inverted cylindrical glow FX
    [SerializeField] MeshRenderer nodeGlowFX = null; //Set active when user enters node area
    [SerializeField] Light nodeSpotlight = null;

    // used by path expansion animation (PathPositiner and PathStretcher)
    public bool isNodeActive = false;

    private void Awake()
    {
        if(nodeGlowFX != null)
        {
            nodeGlowFX.enabled = false;
        }
        if(nodeSpotlight != null)
        {
            nodeSpotlight.enabled = false;
        }
    }

    public Vector3 GetNodeDimensions()
    {
        return nodeFloor.transform.localScale;
    }

    // If user has entered area, turn on visuals and set isNodeActive to true
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Debug.Log("Player entered " + name);
            if (nodeGlowFX != null && !nodeGlowFX.enabled)
            {
                nodeGlowFX.enabled = true;
            }

            if (nodeSpotlight != null && !nodeSpotlight.enabled)
            {
                nodeSpotlight.enabled = true;
            }
            isNodeActive = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (nodeGlowFX != null && !nodeGlowFX.enabled)
            {
                nodeGlowFX.enabled = true;
            }

            if (nodeSpotlight != null && !nodeSpotlight.enabled)
            {
                nodeSpotlight.enabled = true;
            }
            isNodeActive = true;
        }
    }

    // If user exits, turn off visuals and set isNodeActive to false
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player exited " + name);

            if (nodeGlowFX != null)
            {
                if (nodeGlowFX.enabled)
                    nodeGlowFX.enabled = false;
            }

            if (nodeSpotlight != null)
            {
                if (nodeSpotlight.enabled)
                    nodeSpotlight.enabled = false;
            }

            isNodeActive = false;
            //nodeGlowFX.SetActive(false);
        }
    }
}
