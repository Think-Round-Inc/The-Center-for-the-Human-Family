using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class ClosestObjectFinder : MonoBehaviour
{
    public GameObject player;

    [InfoBox("Event fired when at new closest object. GameObject is the painting screen.")]
    public UnityEvent<GameObject> onEnterNewSpot;

    [InfoBox("Event fired when left closest object. GameObject is the painting screen")]
    public UnityEvent<GameObject> onExitNewSpot;

    [Range(0f, 1f)]
    public float facingAccuracy = 0.9f; // Adjust the accuracy of facing the closest object

    [Range(0f, 180f)]
    public float angleThreshold = 45f; // Adjust the angle threshold for "in front" check

    public float distanceThreshold = 3f;

    private List<GameObject> objects;
    private GameObject closestObject;
    private GameObject lastClosestObject;

    private bool hasEnteredSpot = false;

    private void Start()
    {
        objects = new List<GameObject>();
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Screen");
        foreach (GameObject taggedObject in taggedObjects)
        {
            objects.Add(taggedObject);
        }

        lastClosestObject = FindClosestObject();
    }

    private void FixedUpdate()
    {
        closestObject = FindClosestObject();

        if (IsPlayerWithinDistanceThreshold(closestObject) && IsPlayerFacingAndInFront(player, closestObject))
        {
            if (!hasEnteredSpot)
            {
                hasEnteredSpot = true;
                onEnterNewSpot.Invoke(closestObject);
            }
        }
        else
        {
            if (hasEnteredSpot)
            {
                hasEnteredSpot = false;
                onExitNewSpot.Invoke(closestObject);
            }
        }
    }

    private GameObject FindClosestObject()
    {
        if (objects.Count == 0)
        {
            closestObject = null;
            return closestObject;
        }

        closestObject = objects[0];
        float closestDistance = Vector3.Distance(player.transform.position, closestObject.transform.position);

        for (int i = 1; i < objects.Count; i++)
        {
            float distance = Vector3.Distance(player.transform.position, objects[i].transform.position);

            if (distance < closestDistance)
            {
                closestObject = objects[i];
                closestDistance = distance;
            }
        }
        return closestObject;
    }

    private bool IsPlayerWithinDistanceThreshold(GameObject obj)
    {
        float distance = Vector3.Distance(player.transform.position, obj.transform.position);
        return distance <= distanceThreshold;
    }

    private bool IsPlayerFacingAndInFront(GameObject viewer, GameObject target)
    {
        Vector3 directionToTarget = target.transform.position - viewer.transform.position;
        directionToTarget.Normalize();

        float dotProduct = Vector3.Dot(viewer.transform.forward, directionToTarget);
        float angle = Vector3.Angle(viewer.transform.forward, directionToTarget);

        return dotProduct >= facingAccuracy && angle <= angleThreshold;
    }

    public void PrintClosestObject(GameObject gO) => Debug.Log($"Near: {gO.name}");
}
