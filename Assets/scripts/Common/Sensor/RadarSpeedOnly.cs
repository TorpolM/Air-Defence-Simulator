using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSpeedOnly : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public GameObject antenna;
    public float Pt;
    public float dB;
    public float Ft;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        targets.Clear();
    }
    void OnTriggerStay(Collider refrect){
        if(refrect.tag == "Entity"){
            var len = 3e8f / Ft;
		    var rcs = refrect.GetComponent<Entity>().RCS;
            var range = Vector3.Distance(antenna.transform.position,refrect.transform.position);
            var Pr = (Pt * Mathf.Pow(dB,2) * Mathf.Pow(len,2) * rcs) / (Mathf.Pow(4 * Mathf.PI,3) * Mathf.Pow(range,4));
            var PrdB = 10 * Mathf.Log10(Pr);
            Debug.Log(range / 1000);
            targets.Add(refrect.gameObject);
        }
	}
}
