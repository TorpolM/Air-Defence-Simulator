using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorButton : MonoBehaviour
{
    public bool pushed;
    public bool pushInstant;
    public bool lampOn;
    public bool lampOnwithPush;
    public bool flash;
    public float flashInterval;
    Transform knob;
    public AudioClip clip;
    private AudioSource source;
    public Material matOff;
    public Material matOn;

    Material[] mats;
    public string name;
    float lastFlashedTime = 0;

    void Start()
    {
        knob = transform.GetChild(1);
        source = gameObject.GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && pushed){
            source.PlayOneShot(clip);
        }
        if (pushed){
            knob.localPosition = new Vector3(0f,0.0012f,0f);
        } else {
            knob.localPosition = new Vector3(0f,0.0024f,0f);
        }
        if(lampOnwithPush){
            lampOn = pushed;
        }
        pushed = false;

        if(flash && Time.time - lastFlashedTime > flashInterval){
            if(!lampOn){
                lampOn = true;
            } else {
                lampOn = false;
            }
            lastFlashedTime = Time.time;
        }

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
