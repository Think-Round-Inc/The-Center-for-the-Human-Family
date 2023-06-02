using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class TransformUtilities
    {
        public static Vector3 GetRelativeDirectionWithMagnitude(this Transform relativeTransform, float h, float v)
        {
            if (relativeTransform != null)
            {
                //component axes
                Vector3 forward = relativeTransform.forward;
                Vector3 right = relativeTransform.right;

                // zero out y-axis
                forward.y = 0f;
                right.y = 0f;

                return forward.normalized * v + right.normalized * h;
            }
            return Vector3.zero;
        }
    }

    public static class RaycastUtilities
    {
        public static RaycastHit[] SortHitsByDistance(this RaycastHit[] hits)
        {
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            System.Array.Sort(distances, hits);
            return hits;
        }
    }
}