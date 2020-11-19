using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ADP : MonoBehaviour
{
    public float ADPSectorStart;
    public float ADPSectorEnd;
    public GameObject parAntenna;
    public GameObject parforward;
    public GameObject par;
    public GameObject tf;

    public GameObject TCC;
    public GameObject[] screens;

    public bool inADPSector = false;
    List<GameObject> targets = new List<GameObject>();
    public List<Vector3> echopos = new List<Vector3>();
    public List<int> echoIFF = new List<int>();
    public GameObject[] trucks;
    int truckID = 0;
    List<float> targetStrs = new List<float>();
    List<int> targetIFF = new List<int>();
    bool isIFF;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isIFF = TCC.GetComponent<TCC>().isIFFAuto || TCC.GetComponent<TCC>().isIFFSend;


        if(Mathf.Abs(parAntenna.transform.eulerAngles.y - ADPSectorStart) < 1){
            inADPSector = true;
        }
        if(Mathf.Abs(parAntenna.transform.eulerAngles.y - ADPSectorEnd) < 1){
            inADPSector = false;
            float centerX = 0;
            float centerY = 0;
            float avtime = 0;
            int IFF = 0;
            int cnt = 1;
            int cnt2 = 0;
            trucks = GameObject.FindGameObjectsWithTag("ADPTruckFile");
            while(echopos.Count > 0){
                centerX = echopos[0].x;
                centerY = echopos[0].y;
                avtime = echopos[0].z;
                if(TCC.GetComponent<TCC>().isIFFAuto || TCC.GetComponent<TCC>().isIFFSend){
                    if(echoIFF[0] == 1){
                        IFF = 1;
                    }
                }
                cnt = 1;
                if(echopos.Count == 1){
                } else {
                    while(echopos.Count > 1 && Vector2.Distance(echopos[0],echopos[1]) < 5460){
                        centerX += echopos[1].x;
                        centerY += echopos[1].y;
                        avtime += echopos[1].z;
                        cnt += 1;
                        echopos.RemoveAt(1);
                        echoIFF.RemoveAt(1);
                    }
                    centerX = centerX / cnt;
                    centerY = centerY / cnt;
                    avtime = avtime / cnt;
                }
                Vector2 newpos = new Vector2(centerX,centerY);
                float nptime = avtime;
                echopos.RemoveAt(0);
                echoIFF.RemoveAt(0);
                bool isCorrelated = false;
                for(cnt2 = 0;cnt2 < trucks.Length;cnt2++){
                    truckFile truck = trucks[cnt2].GetComponent<truckFile>();
                    if(Vector2.Distance(newpos,truck.currentPos) < 6000){
                        truck.updateTruck(newpos,Time.time - nptime);
                        isCorrelated = true;
                        break;
                    }
                }
                if(!isCorrelated){
                    GameObject truckf = Instantiate(tf);
                    truckf.transform.parent = transform;
                    truckf.GetComponent<truckFile>().fileID = truckID;
                    truckf.GetComponent<truckFile>().truckPos = newpos;
                    truckf.GetComponent<truckFile>().currentPos = newpos;
                    truckf.GetComponent<truckFile>().utime = Time.time;
                    truckf.GetComponent<truckFile>().ID = IFF;
                    foreach(GameObject screen in screens){
                        screen.GetComponent<ADPScreen>().newSymbol(truckf);
                    }
                    truckID += 1;
                }
            }
            echopos.Clear();
        }
        if(inADPSector){
            targets = par.GetComponent<Radar_Advanced>().targets;
            targetStrs = par.GetComponent<Radar_Advanced>().targetStrs;
            targetIFF = par.GetComponent<Radar_Advanced>().targetIFF;
            for(int cnt = 0;cnt < targets.Count;cnt++){
                var target = targets[cnt];
                var PrdB = targetStrs[cnt];
                if(PrdB > -132 && Vector3.Distance(parAntenna.transform.position,target.transform.position) > 20000f){
                    echopos.Add(new Vector3(target.transform.position.x,target.transform.position.z,Time.time));
                    echoIFF.Add(targetIFF[cnt]);
                }
            }
        }
    }
}
