using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] Transform follow = null;

    void Update()
    {
        if(follow != null)
        {
            transform.position = follow.transform.position;
        }
    }
}
