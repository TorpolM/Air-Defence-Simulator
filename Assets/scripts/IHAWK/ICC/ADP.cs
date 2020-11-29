using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ADP :MonoBehaviour{
    public float ADPSectorStart;
    public float ADPSectorEnd;
    public GameObject parAntenna;
    public GameObject par;
    Radar_Advanced _par;
    public GameObject TCC;
    TCC _TCC;
    public HPIR HPIR_A;
    public HPIR HPIR_B;
    public GameObject pointer;
    public GameObject[] screens;
    public GameObject tf;
    public bool inADPSector = false;
    List<GameObject> targets = new List<GameObject>();
    List<int> targetIFF = new List<int>();
    public GameObject[] trucks;
    public Vector2 pointerPos;
    truckFile HPIR_A_Assigned;
    float HPIR_A_Altitude;
    truckFile HPIR_B_Assigned;
    float HPIR_B_Altitude;

    

    Vector3 lastTargetPos;
    int truckID = 0;
    int cnt = 0;
    bool ishooked = false;


    void Start()
    {
        _TCC = TCC.GetComponent<TCC>();
        _par = par.GetComponent<Radar_Advanced>();
        lastTargetPos = par.transform.position;
    }


    void Update()
    {
        if(Mathf.Abs(parAntenna.transform.eulerAngles.y - ADPSectorStart) < 1 && !inADPSector){
            inADPSector = true;
        }
        if(Mathf.Abs(parAntenna.transform.eulerAngles.y - ADPSectorEnd) < 1 && inADPSector){
            inADPSector = false;
        }

        trucks = GameObject.FindGameObjectsWithTag("ADPTruckFile");
        pointerPos = new Vector2(pointer.transform.localPosition.x / -512 * _TCC.scale + parAntenna.transform.position.x,pointer.transform.localPosition.y / 512 * _TCC.scale + parAntenna.transform.position.z);

        if(inADPSector){
            targets = _par.targets;
            targetIFF = _par.targetIFF;
            cnt = 0;
            foreach (GameObject target in targets){
                if(Vector3.Distance(target.transform.position,lastTargetPos) > 5460){
                    Vector2 targetPos = new Vector2(target.transform.position.x,target.transform.position.z); 
                    bool isCorrelated = false;
                    foreach(GameObject truck in trucks){
                        truckFile _truck = truck.GetComponent<truckFile>();
                        if(Vector2.Distance(_truck.currentPos,targetPos) < 6000){
                            _truck.updateTruck(targetPos,0);
                            isCorrelated = true;
                            break;
                        }
                    }
                    if(!isCorrelated){
                        int IFF = 0;
                        if(_TCC.isIFFAuto || _TCC.isIFFSend){
                            if(targetIFF[cnt] == 1){
                                IFF = 1;
                            }
                            if(targetIFF[cnt] == 2){
                                IFF = 2;
                            }
                        }
                        GameObject truckf = Instantiate(tf);
                        truckf.transform.parent = transform;
                        truckf.GetComponent<truckFile>().fileID = truckID;
                        truckf.GetComponent<truckFile>().truckPos = targetPos;
                        truckf.GetComponent<truckFile>().currentPos = targetPos;
                        truckf.GetComponent<truckFile>().utime = Time.time;
                        truckf.GetComponent<truckFile>().ID = IFF;
                        truckf.GetComponent<truckFile>().type = IFF;
                        foreach(GameObject screen in screens){
                            screen.GetComponent<ADPScreen>().newSymbol(truckf);
                        }
                        truckID += 1;
                    }
                    lastTargetPos = target.transform.position;
                }
                cnt += 1;
            }
        }


        ishooked = false;
        foreach(GameObject truck in trucks){
            truckFile _truck = truck.GetComponent<truckFile>();
            Debug.Log(_truck.fileID);
            if(Vector2.Distance(_truck.currentPos,pointerPos) < 2000 && !ishooked){    
                if(_TCC.isIDHost){
                    _truck.ID = 2;
                    _truck.type = 2;
                }
                if(_TCC.isIDFrnd){
                    _truck.ID = 1;
                    _truck.type = 1;
                }
                if(_TCC.isIDUnk){
                    _truck.ID = 0;
                    _truck.type = 0;
                }

                if(_TCC.isAssignLowA && _truck.type != 6 && HPIR_A_Assigned  == null && HPIR_B_Assigned == null){
                    _truck.type = 5;
                    HPIR_A_Altitude = -1111f;
                    HPIR_A_Assigned = _truck;
                    HPIR_A.enable = true;
                    HPIR_A.designating = true;
                }
                

                ishooked = true;
            }
        }


        if(HPIR_A_Assigned != null){
            if(HPIR_A.tracking){
                HPIR_A_Assigned.type = 6;
            } else {
                HPIR_A.designatePos = new Vector3(HPIR_A_Assigned.currentPos.x,HPIR_A_Altitude,HPIR_A_Assigned.currentPos.y);
                HPIR_A.designateRange = Vector3.Distance(HPIR_A.designatePos,parAntenna.transform.position);
            }
        } else{
            HPIR_A.enable = false;
        }
    }
}








/* 
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
        if(Mathf.Abs(parAntenna.transform.eulerAngles.y - ADPSectorEnd) < 1 && inADPSector){
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
*/