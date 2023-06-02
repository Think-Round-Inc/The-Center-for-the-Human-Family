using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathIndicator : MonoBehaviour
{
    [SerializeField] Transform startTransform = null;
    [SerializeField] Transform endTransform = null;
    [SerializeField] Transform visualIndicator = null;

    [SerializeField] float movementSpeedPerc = 1f; // the rate at which the progress increases based on Percentage (not distance)
    [SerializeField] float sinePeriodScalar = 10f; 
    [SerializeField] float magnitudeScalar = 1f; 

    public bool progressingTest = false;
    private bool progressing = false;
    public bool Progressing
    {
        set
        {
            if(!progressing && value)
            {
                //beginning progressing
                visualIndicator.gameObject.SetActive(true);
            }
            progressing = value;
        }
        get
        {
            return progressing;
        }
    }
    public bool looping = false;
    float progress = 0f;
    float offset = 0f;

    private void Start()
    {
        if(visualIndicator != null && startTransform != null && endTransform != null)
        {
            visualIndicator.position = startTransform.position;
            visualIndicator.forward = endTransform.position - startTransform.position;
            endTransform.forward = visualIndicator.forward;
            startTransform.forward = visualIndicator.forward;
        }
    }

    private void OnDrawGizmos()
    {
        if (startTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(startTransform.position, .5f);
        }
        if (endTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(endTransform.position, .5f);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(endTransform.position + (endTransform.right * offset), .5f);
        }
        if(startTransform != null && endTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startTransform.position, endTransform.position);
        }
    }

    void Update()
    {
        if(progressingTest && !progressing)
        {
            Progressing = true;
            progressingTest = false;
        }

        if (!progressing) { return; }

        if (startTransform == null || endTransform == null || visualIndicator == null) { return; } //if any elements missing, return

        //make sure element is visible if it is moving
        visualIndicator.gameObject.SetActive(true);

        //increase progress over time 
        progress += Time.deltaTime * movementSpeedPerc;



        offset = magnitudeScalar * Mathf.Sin(2 * Mathf.PI * progress * sinePeriodScalar);

        //set location of indicator
        visualIndicator.position = Vector3.up + Vector3.Lerp(startTransform.position, endTransform.position, progress) + (endTransform.right * offset);

        //reset progress is over 100%
        if (progress > 1f)
        {
            progress = 0f;
            visualIndicator.gameObject.SetActive(false);

            //if not looping, halt progress
            if (!looping)
            {
                Progressing = false;
            }
        }
    }

    public void SetProgressing(bool progress)
    {
        Progressing = progress;
    }

    public void SetLooping(bool loop)
    {
        looping = loop;
    }
}
