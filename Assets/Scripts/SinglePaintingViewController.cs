using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePaintingViewController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMaterial(Material material)
    {
        Debug.Log(transform.Find("StandMesh").GetComponent<MeshRenderer>().materials[1]);
        Material[] materials = transform.Find("StandMesh").GetComponent<MeshRenderer>().materials;
        materials[1] = material;
        transform.Find("StandMesh").GetComponent<MeshRenderer>().materials = materials;
        Debug.Log(transform.Find("StandMesh").GetComponent<MeshRenderer>().materials[1]);
    }
}
