using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoleRaycaster : MonoBehaviour
{
    public bool reScale = false;
    [Header("Configure")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] float scalar = 1.75f;
    [Header("Reference")]
    [SerializeField] Transform pole = null;


    //Testing
    //public bool gizmos = false;

    //private void OnDrawGizmosSelected()
    //{
    //    if(gizmos)
    //    {
    //        Gizmos.color = Color.magenta;
    //        if (pole != null)
    //            Gizmos.DrawLine(pole.position, pole.position + (pole.forward * (pole.localScale.z + 1f)));
    //    }

    //}

    private void Awake()
    {
        if(Application.isPlaying)
        {
            enabled = false;
        }
    }

    void Update()
    {
        if(reScale)
        {
            reScale = false;
            if (Physics.Raycast(new Ray(pole.position, pole.forward), out RaycastHit info, 100f, layerMask))
            {
                Debug.Log(name + " " + ((info.point - pole.position).magnitude * scalar));
                pole.localScale = new Vector3(1f, 1f, (info.point - pole.position).magnitude * scalar); 
            }
            else
            {
                Debug.Log("Failed To Raycast to Target Layer");
            }
        }

    }
}
