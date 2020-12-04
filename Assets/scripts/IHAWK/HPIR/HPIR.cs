﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPIR : MonoBehaviour
{
    public FCC _FCC;
    public ADP _ADP;
    public Radar_Advanced radar;
    public Transform Azimth;
    public Transform Elevation;
    public Transform Refarence;
    public Vector3 designatePos;
    public float designateRange;
    public float rangeGate;
    public GameObject trackingTarget;
    public bool enable;
    public bool isSearch;
    public bool isLock;
    public bool isGuiding;
    public bool isLost;
    public bool ModeAuto;
    public bool ModeAssign;
    bool lastTracking;
    Quaternion standbyPos;
    float azimthOffset;
    float elevationOffset;
    float theta = 0;
    public Vector3 TrackPos;
    public float targetStr;
    public float targetAlt;
    public float targetSpd;
    
    public List<GameObject> missiles = new List<GameObject>();
    void Start()
    {
        standbyPos = Refarence.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        targetStr = -100;
        targetSpd = 0;
        if(enable){
            TrackPos = new Vector3(-999999,-999999,-999999);
            if(isSearch){
                Refarence.rotation = Quaternion.Slerp(Refarence.rotation,Quaternion.LookRotation(designatePos - transform.position),0.5f * Time.deltaTime);
                azimthOffset = Mathf.Sin(Mathf.Deg2Rad * theta);
                if(designatePos.y == -1111){
                    Refarence.localEulerAngles = new Vector3(-3f,Refarence.localEulerAngles.y,Refarence.localEulerAngles.z);
                    elevationOffset = Mathf.Cos(Mathf.Deg2Rad * theta) * -3f;
                }
                if(designatePos.y == -2222){
                    Refarence.localEulerAngles = new Vector3(-25.5f,Refarence.localEulerAngles.y,Refarence.localEulerAngles.z);
                    elevationOffset = Mathf.Cos(Mathf.Deg2Rad * theta) * -19.5f;
                }
                foreach(RadarData target in radar.targetData){
                    if(Mathf.Abs(Vector3.Distance(target.position,transform.position) - designateRange) < 1000){
                        TrackPos = target.position;
                        rangeGate = Vector3.Distance(target.position,transform.position);
                        isSearch = false;
                        isLock = true;
                        isLost = false;
                        Refarence.transform.localEulerAngles += new Vector3(elevationOffset,azimthOffset,0);
                        azimthOffset = 0;
                        elevationOffset = 0;
                        break;
                    }
                }
            } else {
                azimthOffset = 0;
                elevationOffset = 0;
            }
            var index = 0;
            targetStr = 0;
            if(isLock){
                foreach(RadarData target in radar.targetData){
                    if(Mathf.Abs(Vector3.Distance(target.position,transform.position) - rangeGate) < 1000){
                        TrackPos = target.position;
                        rangeGate = Vector3.Distance(target.position,transform.position);
                        isLock = true;
                        targetAlt = TrackPos.y;
                        targetStr = target.SignalStr;
                        targetSpd = target.velocity.magnitude * 3.6f;
                        break;
                    }
                    index += 1;
                }
                Refarence.rotation = Quaternion.Slerp(Refarence.rotation,Quaternion.LookRotation(TrackPos - transform.position),5f * Time.deltaTime);
            }
            if(TrackPos == new Vector3(-999999,-999999,-999999)){
                isLock = false;
            }
            if(lastTracking && !isLock){
                isLost = true;
            }
            if(missiles.Count > 0){
                isGuiding = true;
            } else {
                isGuiding = false;
            }
        } else {
            trackingTarget = null;
            isSearch = false;
            isLost = false;
            isLock = false;
            targetAlt = 0;
            Refarence.transform.rotation = Quaternion.Slerp(Refarence.transform.rotation,standbyPos,1f * Time.deltaTime);
            rangeGate = 80000;
        }


        if(_FCC.isBreakLock){
            enable = false;
        }


        theta += 180 * Time.deltaTime;

        Azimth.transform.localEulerAngles = new Vector3(0,Refarence.localEulerAngles.y + azimthOffset,0);
        Elevation.transform.localEulerAngles = new Vector3(Refarence.localEulerAngles.x + elevationOffset,0,0);
    }
}
