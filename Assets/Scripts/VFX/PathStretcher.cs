using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//
public class PathStretcher : MonoBehaviour
{
    [Header("State")]
    [SerializeField] [Range(0.02f, 1f)] float progress = 1f;
    public bool expanded = false;
    public bool expanding = false;
    public bool shrinking = false;

    [Header("Testing")]
    public bool testing = false;
    public bool testExpand = false;
    public bool testShrink = false;

    [Header("Path Configure")]
    [SerializeField] float speed = 0f;
    float minimumLength = 0.02f;

    [Header("Reference")]
    [SerializeField] PathPositioner pathPositioner = null;
    [SerializeField] Transform pathToStretch = null;
    [SerializeField] GameObject startPointPartcles = null;
    [SerializeField] PathStretcher oppositeDirectionPathPartner = null;

    [Header("Camera Properies")]
    [SerializeField] float cameraFocusRadiusThreshold = .75f;//how close to center of screen must path be in order to expand? (percentage of screen)
    [SerializeField] float delay = .5f;
    float cumulativeDelay = 0f;
    public bool canExpand = false;

    // Cache
    Vector3 startPoint = new Vector3();
    Vector3 endPoint = new Vector3();




    private void OnDrawGizmosSelected()
    {
        if(pathPositioner != null)
        {
            Gizmos.color = Color.green;
            //Vector3 startingPoint = pathPositioner.GetStartingPoint(out Vector3 forward);
            Gizmos.DrawWireSphere(startPoint, .5f);

            //Vector3 endPoint = pathPositioner.GetEndPoint();
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(endPoint, .5f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPoint, endPoint);
        }
    }

    private void Awake()
    {
        if (pathPositioner == null)
        {
            pathPositioner = GetComponent<PathPositioner>();
        }

        if (pathPositioner != null)
        {
            pathPositioner.UpdateReferences();
            startPoint = pathPositioner.GetStartingPoint();
            endPoint = pathPositioner.GetEndPoint();
        }

        if(Application.isPlaying) // when application starts playing, all paths should be retracted
        {
            ResetPath();
        }
        else // if in the editor, all paths should be expanded
        {
            SetPathExpanded();
        }

    }



    void Update()
    {
        //test once
        if (testExpand)
        {
            testExpand = false;
            if (shrinking)
            {
                shrinking = false;
            }

            expanding = true;
        }

        if (testShrink)
        {
            testShrink = false;
            if (expanding)
            {
                expanding = false;
            }

            shrinking = true;
        }

        if(Application.isPlaying)
        {
            //only if user is at From node
            if (pathPositioner.IsNodeActive && !expanded
                // if user is near the unexpanded path, or looking at the unexpanded path or the next node
                && (PlayerController.Instance != null && Vector3.Distance(PlayerController.Instance.transform.position, transform.position) < 1f
                    || IsOnScreen(transform) || IsOnScreen(pathPositioner.GetNodeTo()))
                )
            {
                //add a small delay before path expands
                if(cumulativeDelay > delay)
                {
                    //Debug.Log(name + " is onScreen and expanding");
                    expanding = true;
                    startPointPartcles.SetActive(false);
                    Expand();
                    cumulativeDelay = 0f;
                }
                else
                {
                    cumulativeDelay += Time.deltaTime;
                }
            }
            else
            {
                cumulativeDelay = 0f;
            }
        }

        if (expanding)
        {
            Expand();
        }
        else if (shrinking)
        {
            Shrink();
        }
        else if (testing)
        {
            SetSize(progress);
        }
    }


    // Resets the path to an unexpanded state
    private void ResetPath()
    {
        SetSize(minimumLength);

        if (startPointPartcles != null) 
            startPointPartcles.SetActive(true);

        if(Application.isPlaying)
        {
            //make sure opposite path is turned on
            if (oppositeDirectionPathPartner != null)
            {
                oppositeDirectionPathPartner.gameObject.SetActive(true);
            }
        }


        expanded = false;
    }

    private void SetPathExpanded()
    {
        SetSize(1f);

        if (Application.isPlaying)
        {
            //Debug.Log("Playing");
            //Disable opposite path because user does not need it anymore
            if (oppositeDirectionPathPartner != null)
            {
                oppositeDirectionPathPartner.gameObject.SetActive(false);
            }
        }
        else
        {
            //Debug.Log("Not Playing");
        }

        expanded = true;
    }

    private bool IsOnScreen(Transform t)
    {
        //is the transform on screen and "close enough" to the center of screen?
        Vector3 screenMidPoint = Camera.main.WorldToViewportPoint(t.position);
        return screenMidPoint.x > (/*0f + */.5f * cameraFocusRadiusThreshold)
            && screenMidPoint.x < (.5f  +  (.5f * cameraFocusRadiusThreshold))
            && screenMidPoint.y > (/*0f + */.5f * cameraFocusRadiusThreshold)
            && screenMidPoint.y < (.5f  +  (.5f * cameraFocusRadiusThreshold));
    }

    public void Expand() //continuously expand until at the end
    {
        //Debug.Log("Expanding");
        progress += Time.deltaTime * speed;
        if(progress >= 1f)
        {
            progress = 1f;
            expanding = false;
            SetPathExpanded();
        }
        expanded = true;
        SetSize(progress);
    }

    public void Shrink() //continuously shrink until scale is 0
    {
        //Debug.Log("Shrinking");
        progress -= Time.deltaTime * speed;
        if (progress <= minimumLength)
        {
            progress = minimumLength;
            shrinking = false;
        }
        expanded = false;
        SetSize(progress);
    }

    private void SetSize(float percentage)
    {
        if (pathToStretch != null)
        {
            progress = percentage;
            Vector3 newScale = new Vector3(pathToStretch.transform.localScale.x, pathToStretch.transform.localScale.y, percentage);
            pathToStretch.transform.localScale = newScale;
        }
    }
}
