using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class ClosestObjectFinder : MonoBehaviour
{
    public GameObject player;
    public UnityEvent<GameObject> onEnterNewSpot;
    public float distanceThreshold = 3f;

    private List<GameObject> objects;
    private GameObject closestObject;
    private GameObject lastClosestObject;

    private bool hasEnteredSpot = false;

    private void Start()
    {
        objects = new List<GameObject>();
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Stand");
        foreach (GameObject taggedObject in taggedObjects)
        {
            objects.Add(taggedObject);
        }

        lastClosestObject = FindClosestObject();
    }

    private void FixedUpdate()
    {
        closestObject = FindClosestObject();

        if (!hasEnteredSpot && IsPlayerWithinDistanceThreshold(closestObject))
        {
            hasEnteredSpot = true;
            onEnterNewSpot.Invoke(closestObject);
        }
        else if (hasEnteredSpot && closestObject != lastClosestObject && IsPlayerWithinDistanceThreshold(closestObject))
        {
            lastClosestObject = closestObject;
            onEnterNewSpot.Invoke(closestObject);
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

    public void PrintClosestObject(GameObject gO) => Debug.Log($"Near: {gO.name}");
}
