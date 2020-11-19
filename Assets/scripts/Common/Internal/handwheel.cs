using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handwheel : MonoBehaviour
{
    // Start is called before the first frame update
    public float position;
    public float rotateSpd;
    public float wheelrotatemag;
    public bool useRange;
    public float min;
    public float max;

    public AudioClip[] sources;
    AudioSource source;
    Transform knob;
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        knob = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        knob.localEulerAngles = new Vector3(-90f,0f,position * wheelrotatemag);
    }

    public void SoundCW(){
        //AudioClip clip = sources[0];
        //source.PlayOneShot(clip);
        if(useRange){
            if (position > min + 2){
                if(Input.GetKey(KeyCode.LeftShift)){
                    position += -1 * Time.deltaTime * rotateSpd * 2;
                } else {
                    position += -1 * Time.deltaTime * rotateSpd;
                }
            }
        } else {
            if(Input.GetKey(KeyCode.LeftShift)){
                position += -1 * Time.deltaTime * rotateSpd * 2;
            } else {
                position += -1 * Time.deltaTime * rotateSpd;
            }
        }
    }
    public void SoundCCW(){
        //AudioClip clip = sources[1];
        //source.PlayOneShot(clip);
       if(useRange){
            if (position < max - 2){
                if(Input.GetKey(KeyCode.LeftShift)){
                    position += 1 * Time.deltaTime * rotateSpd * 2;
                } else {
                    position += 1 * Time.deltaTime * rotateSpd;
                }
            }
        } else {
            if(Input.GetKey(KeyCode.LeftShift)){
                position += 1 * Time.deltaTime * rotateSpd * 2;
            } else {
                position += 1 * Time.deltaTime * rotateSpd;
            }
        }
    }
}
