using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    public bool pushed;
    bool pushinstant;
    public AudioClip clip;
    Transform knob;
    AudioSource source;
    public string name;
    public bool lastpushed;
    void Start()
    {
        knob = ((transform.GetChild(0)).transform.GetChild(0)).transform.GetChild(0);
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
            knob.localPosition = new Vector3(0f,0.0036f,0f);
        } else {
            knob.localPosition = new Vector3(0f,0.007183165f,0f);
        }
        lastpushed = pushed;
        pushed = false;
    }
}
