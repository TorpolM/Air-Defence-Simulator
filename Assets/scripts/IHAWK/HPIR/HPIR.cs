using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPIR : MonoBehaviour
{
    // Start is called before the first frame update
    public Radar_Advanced radar;
    public Transform Azimth;
    public Transform Elevation;
    public Transform Refarence;
    public Vector3 designatePos;
    public float designateRange;
    public float rangeGate;
    public GameObject trackingTarget;
    public bool enable;
    public bool designating;
    public bool tracking;
    public bool isGuiding;
    public bool isLost;
    bool lastTracking;
    Quaternion standbyPos;
    float azimthOffset;
    float elevationOffset;
    float theta = 0;
    Vector3 TrackPos;
    
    public List<GameObject> missiles = new List<GameObject>();
    void Start()
    {
        standbyPos = Refarence.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(enable){
            trackingTarget = null;
            if(designating){
                Refarence.rotation = Quaternion.Slerp(Refarence.rotation,Quaternion.LookRotation(designatePos - transform.position),0.5f * Time.deltaTime);
                azimthOffset = Mathf.Sin(Mathf.Deg2Rad * theta);
                if(designatePos.y == -1111){
                    Refarence.localEulerAngles = new Vector3(-5f,Refarence.localEulerAngles.y,Refarence.localEulerAngles.z);
                    elevationOffset = Mathf.Cos(Mathf.Deg2Rad * theta) * -5f;
                }
                if(designatePos.y == -2222){
                    Refarence.localEulerAngles = new Vector3(-35f,Refarence.localEulerAngles.y,Refarence.localEulerAngles.z);
                    elevationOffset = Mathf.Cos(Mathf.Deg2Rad * theta) * -25f;
                }
                foreach(GameObject target in radar.targets){
                    if(Mathf.Abs(Vector3.Distance(target.transform.position,transform.position) - designateRange) < 1000){
                        trackingTarget = target;
                        TrackPos = trackingTarget.transform.position;
                        rangeGate = Vector3.Distance(target.transform.position,transform.position);
                        designating = false;
                        tracking = true;
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
            if(tracking){
                foreach(GameObject target in radar.targets){
                    if(Mathf.Abs(Vector3.Distance(target.transform.position,transform.position) - rangeGate) < 1000){
                        trackingTarget = target;
                        TrackPos = trackingTarget.transform.position;
                        rangeGate = Vector3.Distance(target.transform.position,transform.position);
                        tracking = true;
                        break;
                    }
                }
                Refarence.rotation = Quaternion.Slerp(Refarence.rotation,Quaternion.LookRotation(TrackPos - transform.position),5f * Time.deltaTime);
            }
            if(trackingTarget == null){
                tracking = false;
            }
            if(lastTracking && !tracking){
                isLost = true;
            }
            if(missiles.Count > 0){
                isGuiding = true;
            } else {
                isGuiding = false;
            }
        } else {
            trackingTarget = null;
            designating = false;
            isLost = false;
            tracking = false;
            Refarence.transform.rotation = Quaternion.Slerp(Refarence.transform.rotation,standbyPos,1f * Time.deltaTime);
            rangeGate = 100000;
        }
        theta += 180 * Time.deltaTime;

        Azimth.transform.localEulerAngles = new Vector3(0,Refarence.localEulerAngles.y + azimthOffset,0);
        Elevation.transform.localEulerAngles = new Vector3(Refarence.localEulerAngles.x + elevationOffset,0,0);
    }
}
