using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_Advanced : MonoBehaviour
{
    public List<RadarData> targetData = new List<RadarData>();
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
    Entity entity;
    RadarData data;
    
    void Start()
    {
        calcParameters(dB,Ft);
        data = new RadarData();
    }
    void FixedUpdate()
    {
        targetData.Clear();
    }
    void OnTriggerStay(Collider refrect){
        if(refrect.tag == "Entity"){
            entity = refrect.GetComponent<Entity>();
            rcs = entity.RCS;
            range = Vector3.Distance(antenna.transform.position,refrect.transform.position);
            Pr = Pt * Mathf.Pow(gain,2) * Mathf.Pow(len,2) * rcs / ((Mathf.Pow(4*3.14f,3)*Mathf.Pow(range,4)));
            PrdB = 10 * Mathf.Log10(Pr);
            data.position = refrect.transform.position;
            data.velocity = refrect.GetComponent<Rigidbody>().velocity;
            data.SignalStr = PrdB;
            data.IFF = entity.side;
            targetData.Add(data);
        }
	}


    void calcParameters(float dB,float Ft){
        len = 3e8f / Ft;
        gain = Mathf.Pow(10,dB / 10);
    }
}
