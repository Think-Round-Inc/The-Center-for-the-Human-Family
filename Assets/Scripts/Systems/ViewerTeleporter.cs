using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerTeleporter : MonoBehaviour
{
    [SerializeField] Transform[] viewerTeleportLocations;
    [SerializeField] GameObject viewer;

    public void TeleportViewerToLocation(int targetLocation)
    {
        if (targetLocation < viewerTeleportLocations.Length && viewerTeleportLocations[targetLocation] != null)
        {
            viewer.transform.position = viewerTeleportLocations[targetLocation].position;
            viewer.transform.forward = viewerTeleportLocations[targetLocation].forward;
        }
    }
}
