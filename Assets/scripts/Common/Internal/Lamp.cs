using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    // Start is called before the first frame update
    public Material matOff;
    public Material matOn;
    public bool enable;
    Material[] mats;
    SkinnedMeshRenderer sMeshRender;
    MeshRenderer meshRenderer;
    public bool isSkinnedMesh;
    void Start()
    {
        sMeshRender = GetComponent<SkinnedMeshRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSkinnedMesh){
            mats = GetComponent<SkinnedMeshRenderer>().materials;
        } else {
            mats = GetComponent<MeshRenderer>().materials;
        }
        if (enable){
            mats[0] = matOn;
        } else {
            mats[0] = matOff;
        }
        if(isSkinnedMesh){
            GetComponent<SkinnedMeshRenderer>().materials = mats;
        } else {
            GetComponent<MeshRenderer>().materials = mats;
        }
    }
}
