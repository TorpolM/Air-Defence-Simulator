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
    List<RadarData> targets = new List<RadarData>();
    RadarData tgt;
    public GameObject[] trucks;
    public Vector2 pointerPos;
    public Vector2 OSVRPos;
    Vector2 PltPos;
    truckFile HPIR_A_Assigned;
    float HPIR_A_Altitude;
    truckFile HPIR_B_Assigned;
    float HPIR_B_Altitude;

    truckFile PlatoonThreat;
    truckFile OSVRThreat;

    

    Vector3 lastTargetPos;
    int truckID = 0;
    int cnt = 0;
    bool ishooked = false;


    void Start()
    {
        _TCC = TCC.GetComponent<TCC>();
        _par = par.GetComponent<Radar_Advanced>();
        lastTargetPos = par.transform.position;
        OSVRPos = new Vector2(-30000,0);
        PltPos = new Vector2(parAntenna.transform.position.x,parAntenna.transform.position.z);
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
            targets = _par.targetData;
            cnt = 0;
            foreach (RadarData target in targets){
                if(Vector3.Distance(target.position,lastTargetPos) > 5460){
                    Vector2 targetPos = new Vector2(target.position.x,target.position.z); 
                    bool isCorrelated = false;
                    foreach(GameObject truck in trucks){
                        truckFile _truck = truck.GetComponent<truckFile>();
                        if(Vector2.Distance(_truck.currentPos,targetPos) < 6000){
                            _truck.updateTruck(targetPos,0);
                            isCorrelated = true;
                            break;
                        }
                    }
                    if(!isCorrelated && trucks.Length < 16){
                        int IFF = 0;
                        if(_TCC.isIFFAuto || _TCC.isIFFSend){
                            if(target.IFF == 1){
                                IFF = 1;
                            }
                            if(target.IFF == 2){
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
                        foreach(GameObject screen in screens){
                            screen.GetComponent<ADPScreen>().newSymbol(truckf);
                        }
                        truckID += 1;
                    }
                    lastTargetPos = target.position;
                }
                cnt += 1;
            }
        }


        ishooked = false;
        var OSVRThreatRange = 1000000f;
        var PlatoonThreatRange = 1000000f;
        foreach(GameObject truck in trucks){
            truckFile _truck = truck.GetComponent<truckFile>();
            if(Vector2.Distance(_truck.currentPos,pointerPos) < 2000 && !ishooked && !_truck.isTracked){    
                if(_TCC.isIDHost){
                    _truck.ID = 2;
                }
                if(_TCC.isIDFrnd){
                    _truck.ID = 1;
                }
                if(_TCC.isIDUnk){
                    _truck.ID = 0;
                }

                if(_TCC.isAssignLowA && !_truck.isTracked && HPIR_A_Assigned == null && HPIR_B_Assigned != _truck){
                    _truck.isAssigned = true;
                    HPIR_A_Altitude = -1111f;
                    HPIR_A_Assigned = _truck;
                    HPIR_A.enable = true;
                    HPIR_A.isSearch = true;
                }
                if(_TCC.isAssignHighA && !_truck.isTracked && HPIR_A_Assigned == null && HPIR_B_Assigned != _truck){
                    _truck.isAssigned = true;
                    HPIR_A_Altitude = -2222f;
                    HPIR_A_Assigned = _truck;
                    HPIR_A.enable = true;
                    HPIR_A.isSearch = true;
                }
                ishooked = true;
            }
            _truck.isPltThreat = false;
            PlatoonThreat = null;
            if(Vector2.Distance(_truck.currentPos,PltPos) < PlatoonThreatRange && _truck.ID == 2){
                PlatoonThreatRange = Vector2.Distance(_truck.currentPos,PltPos);
                PlatoonThreat = _truck;
            }
            _truck.isOSVRThreat = false;
            OSVRThreat = null;
            if(Vector2.Distance(_truck.currentPos,OSVRPos) < OSVRThreatRange && _truck.ID == 2){
                OSVRThreatRange = Vector2.Distance(_truck.currentPos,OSVRPos);
                OSVRThreat = _truck;
            }
        }

        if(PlatoonThreat != null){
            PlatoonThreat.isPltThreat = true;
        }
        if(OSVRThreat != null && OSVRThreat != PlatoonThreat){
            OSVRThreat.isOSVRThreat = true;
        }


        if(HPIR_A_Assigned != null){
            if(HPIR_A.isLock){
                HPIR_A_Assigned.isTracked = true;
                Vector2 targetPos = new Vector2(HPIR_A.TrackPos.x,HPIR_A.TrackPos.z); 
                HPIR_A_Assigned.updateTruck(targetPos,0);
            } else {
                HPIR_A.designatePos = new Vector3(HPIR_A_Assigned.currentPos.x,HPIR_A_Altitude,HPIR_A_Assigned.currentPos.y);
                HPIR_A.designateRange = Vector3.Distance(HPIR_A.designatePos,parAntenna.transform.position);
            }
            HPIR_A.ModeAuto = true;
        }
        if(!HPIR_A.enable && HPIR_A_Assigned != null){
            Destroy(HPIR_A_Assigned.gameObject);
            HPIR_A_Assigned = null;
        }
        if(!HPIR_A.enable){
            HPIR_A.ModeAuto = false;
        }
    }
}