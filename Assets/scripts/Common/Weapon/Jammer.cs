using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jammer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    Transform antenna;
    public bool enable;
    Radar_Advanced targetRdr;
    RadarData data;
    void Start(){
        targetRdr = target.GetComponent<Radar_Advanced>();
        antenna = target.transform.parent;
        data = new RadarData();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null && enable){
            targetRdr = target.GetComponent<Radar_Advanced>();
            if(Vector3.Angle(antenna.transform.position - transform.position,antenna.transform.forward) > 150){
                for(int cnt = 0;cnt < 4;cnt++){
                    Debug.Log("Jamming!");
                    data.position = transform.position + Vector3.Normalize(antenna.transform.position - transform.position) * Random.Range(-3000,3000);
                    data.velocity = new Vector3(0,0,0);
                    data.SignalStr = Random.Range(-95,-70);
                    data.IFF = -1;
                    targetRdr.targetData.Add(data);
                }
            }
            
        }
    }
}
