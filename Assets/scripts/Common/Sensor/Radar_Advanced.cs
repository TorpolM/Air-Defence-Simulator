using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_Advanced : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public List<float> targetStrs = new List<float>();
    public List<int> targetIFF = new List<int>();
    public GameObject antenna;
    public float Pt;
    public float dB;
    public float Ft;
    float rcs;
    public float len;
    public float gain;
    float range;
    float Pr;
    float PrdB;
    void Start()
    {
        calcParameters(dB,Ft);
    }
    void FixedUpdate()
    {
        targets.Clear();
        targetStrs.Clear();
        targetIFF.Clear();
    }
    void OnTriggerStay(Collider refrect){
        if(refrect.tag == "Entity"){
            rcs = refrect.GetComponent<Entity>().RCS;
            range = Vector3.Distance(antenna.transform.position,refrect.transform.position);
            Pr = Pt * Mathf.Pow(gain,2) * Mathf.Pow(len,2) * rcs /((Mathf.Pow(4*3.14f,3)*Mathf.Pow(range,4)));
            PrdB = 10 * Mathf.Log10(Pr);
            targets.Add(refrect.gameObject);
            targetStrs.Add(PrdB);
            targetIFF.Add(refrect.GetComponent<Entity>().side);
        }
	}


    void calcParameters(float dB,float Ft){
        len = 3e8f / Ft;
        gain = Mathf.Pow(10,dB / 10);

    }
}
