using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[SelectionBase]
//
public class PathPositioner : MonoBehaviour
{
    /// <summary>
    /// This class positions path objects in between start and end points indicated by names of nodes in gameObject name with the format "FromNode_ToNode"
    /// These paths must also overlap each other on different levels. Configuration for height levels has been provided. 
    /// Resulting y-height: globalHeightBuffer + heightBuffer * heightLayer
    /// </summary>

    [Header("State")]
    public bool position = false;
    public bool positionContinuous = false;
    public bool IsNodeActive
    {
        get => nodeFrom != null && nodeFrom.isNodeActive;
    }

    [Header("Height Configuration")]
    [Tooltip("Lowest Number = Lowest Height")] [SerializeField] int heightLayer = 1;
    [SerializeField] float globalHeightBuffer = .001f;
    float heightBuffer = .001f;


    [Header("Reference")]
    [SerializeField] SectionLayoutManager layoutManager = null;
    [SerializeField] SectionNode nodeFrom = null;
    [SerializeField] SectionNode nodeTo = null;
    [SerializeField] Transform pathScale = null;


    //Cache 
    NodeEnum nodeFromEnum = NodeEnum.Default;
    NodeEnum nodeToEnum = NodeEnum.Default;
    Vector3 forward = Vector3.zero;
    Vector3 ending = Vector3.zero;
    Vector3 starting = Vector3.zero;

    private void Start()
    {
        if(layoutManager == null)
        {
            layoutManager = GetComponentInParent<SectionLayoutManager>();
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (nodeFrom != null && nodeTo != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(nodeFrom.transform.position, nodeTo.transform.position);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(ending, .5f);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(starting, .5f);
        }
    }

    void Update()
    {
        if(nodeFrom == null || nodeTo == null)
        {
            UpdateReferences();
        }



        if (positionContinuous)
        {
            Position();
        }
        else if (position)
        {
            position = false;
            Position();
        }
    }

    public void UpdateReferences(PathPositioner pathPos)
    {

    }
    public void UpdateReferences()
    {
        if (layoutManager == null)
        {
            layoutManager = GetComponentInParent<SectionLayoutManager>();
        }
        if (layoutManager != null)
        {
            string[] nameStrings = name.Split('_');
            //Debug.Log(nameStrings[0] + "," + nameStrings[1]);

            nodeFromEnum = (NodeEnum)Enum.Parse(typeof(NodeEnum), nameStrings[0]);
            nodeToEnum = (NodeEnum)Enum.Parse(typeof(NodeEnum), nameStrings[1]);

            nodeFrom = layoutManager.GetNode(nodeFromEnum);
            nodeTo = layoutManager.GetNode(nodeToEnum);

            starting = nodeFrom.transform.position;
            ending = nodeTo.transform.position;
            forward = (nodeTo.transform.position - nodeFrom.transform.position).normalized;
        }
        else //todo - if not in prefab editor
        {
            Debug.LogWarning("Path (" + name + ") cannot align itself because LayoutManager reference is null (Located on Section GameObject)");
        }
    }

    public Transform GetNodeTo()
    {
        return nodeTo.transform;
    }

    private void Position()
    {
        if (nodeFrom != null && nodeTo != null)
        {
            Vector3 forwardVector = nodeTo.transform.position - nodeFrom.transform.position;
            if (pathScale != null)
            {
                pathScale.localScale = new Vector3(1f, 1f, forwardVector.magnitude);
            }
            forward = forwardVector.normalized;
            transform.forward = forward;
            transform.position = GetStartingPoint();
        }
    }

    // Position using acquired Node references

    private float GetBuffer(SectionNode node)
    {
        return node.GetNodeDimensions().x / 2f;
    }

    public Vector3 GetEndPoint()
    {

        float buffer = GetBuffer(nodeTo) - .5f;

        Vector3 endingPosition = nodeTo.transform.position + (-forward * (buffer - .1f));
        endingPosition += Vector3.up * (heightBuffer + (globalHeightBuffer * (1 + heightLayer)));
        ending = endingPosition;
        return endingPosition;
    }

    public Vector3 GetStartingPoint()
    {
        float buffer = GetBuffer(nodeFrom) - .5f;

        Vector3 startingPosition = nodeFrom.transform.position + (forward * (buffer - .1f));
        startingPosition += Vector3.up * (heightBuffer + (globalHeightBuffer * (1 + heightLayer)));
        starting = startingPosition;
        return startingPosition;
    }


}
