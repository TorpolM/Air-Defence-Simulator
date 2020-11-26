using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorButton : MonoBehaviour
{
    public bool pushed;
    public bool pushInstant;
    public bool lampOn;
    public bool lampOnwithPush;
    Transform knob;
    private AudioSource[] sources;
    public Material matOff;
    public Material matOn;

    Material[] mats;
    public string name;

    void Start()
    {
        knob = transform.GetChild(1);
        sources = gameObject.GetComponents<AudioSource>();
    }
    void FixedUpdate()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pushed){
            knob.localPosition = new Vector3(0f,0.0012f,0f);
        } else {
            knob.localPosition = new Vector3(0f,0.0024f,0f);
        }
        if(lampOnwithPush){
            lampOn = pushed;
        }
        pushed = false;

        mats = knob.GetComponent<MeshRenderer>().materials;
        if (lampOn){
            mats[0] = matOn;
            mats[1] = mats[1];
            mats[2] = mats[2];
        } else {
            mats[0] = matOff;
            mats[1] = mats[1];
            mats[2] = mats[2];
        }
        knob.GetComponent<MeshRenderer>().materials = mats;
    }
}
