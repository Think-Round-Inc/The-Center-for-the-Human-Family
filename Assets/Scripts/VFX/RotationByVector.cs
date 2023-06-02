using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationByVector : MonoBehaviour
{
    [SerializeField] Vector3 rotationVector = new Vector3();

    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime);
    }
}
