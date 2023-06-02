using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChildrenPositioner : MonoBehaviour
{
    public enum PositionType { Line, Ring }
    public enum DirectionType { X, Y, Z, Parent }

    [Header("Confirm")]
    [SerializeField] PositionType positionType = PositionType.Ring;
    [SerializeField] float increment = 1f;
    [SerializeField] float buffer = 10f;
    [SerializeField] DirectionType axis = DirectionType.Z;
    [SerializeField] bool negateAxis = false;
    [SerializeField] bool childrenFaceDirection = false;


    [Header("Testing")]
    public bool reposition = false;
    public bool rotate = false;
    public bool displayGizmos = true;
    [SerializeField] float gizmoRadius = 1f;
    [Header("Reference")]
    [SerializeField] Transform parent = null;

    private void Awake()
    {
        if (parent == null)
        {
            parent = transform;
        }
            
    }
    private void OnDrawGizmosSelected()
    {
        if(displayGizmos && parent != null)
        {
            foreach (Transform child in parent)
            {
                Gizmos.DrawSphere(child.position, gizmoRadius);
            }
        }

    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            if (reposition)
            {
                reposition = false;

                switch (positionType)
                {
                    case PositionType.Ring:
                        PositionRing();
                        break;
                    case PositionType.Line:
                        PositionLine();
                        break;
                }
            }
            if(rotate)
            {
                rotate = false;
                RotateAll();
            }
        }
        else
        {
            enabled = false;
        }
    }


    private void PositionLine()
    {
        foreach (Transform child in parent)
        {
            Vector3 direction = Vector3.zero;
            switch (axis)
            {
                case DirectionType.Z:
                    direction = Vector3.forward;
                    break;
                case DirectionType.X:
                    direction = Vector3.right;
                    break;
                case DirectionType.Y:
                    direction = Vector3.up;
                    break;
                case DirectionType.Parent:
                    direction = (parent.position - child.position).normalized;
                    break;
            }
            child.localPosition = /*transform.position +*/ (direction * buffer) + (direction * increment * child.GetSiblingIndex());

            if(childrenFaceDirection)
            {
                float scalar = negateAxis ? -1f : 1f;
                switch (axis)
                {
                    case DirectionType.Z:
                        child.forward = scalar * parent.forward;
                        break;
                    case DirectionType.X:
                        child.forward = scalar * parent.right;
                        break;
                    case DirectionType.Y:
                        child.forward = scalar * parent.up;
                        break;
                    case DirectionType.Parent:
                        child.forward = scalar * (parent.position - child.position).normalized;
                        break;
                }
            }
        }
    }

    private void PositionRing()
    {
        foreach (Transform child in transform)
        {
            child.transform.rotation = Quaternion.Euler(new Vector3(0f, (360f / parent.childCount) * child.GetSiblingIndex(), 0f));
            child.transform.position = parent.position + child.transform.forward * buffer;
            if(childrenFaceDirection)
            {
                float scalar = negateAxis ? -1f : 1f;
                switch (axis)
                {
                    case DirectionType.Z:
                        child.forward = scalar * child.transform.forward;
                        break;
                    case DirectionType.X:
                        child.forward = scalar * child.transform.right;
                        break;
                    case DirectionType.Y:
                        child.forward = scalar * child.transform.up;
                        break;
                    case DirectionType.Parent:
                        child.forward = scalar * (parent.position - child.position).normalized;
                        break;
                }
            }
        }
    }

    private void RotateAll()
    {
        foreach (Transform child in transform)
        {
            Rotate(child);
        }
    }

    private void Rotate(Transform child)
    {
        if (childrenFaceDirection)
        {
            float scalar = negateAxis ? -1f : 1f;
            switch (axis)
            {
                case DirectionType.Z:
                    child.Rotate(scalar * Vector3.forward);
                    break;
                case DirectionType.X:
                    child.Rotate(scalar * Vector3.right);
                    break;
                case DirectionType.Y:
                    child.Rotate(scalar * Vector3.up);
                    break;
            }
        }
    }
}
