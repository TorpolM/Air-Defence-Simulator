using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMFCS : MonoBehaviour
{
    public GameObject radar;
    public Transform Azimth;
    public Transform Elevation;
    public Transform Refarence;
    public GameObject cScanAxis;
    public Vector3 designatePos;
    public float rangeGate;
    public GameObject trackingTarget;
    public bool enable;
    public bool slaving;
    public bool tracking;
    public int direction;
    public bool isGuiding;
    public bool isLost;
    
    public List<GameObject> missiles = new List<GameObject>();

    Radar_Advanced radarData;
    bool lastTracking;
    Quaternion standbyPos;
    float azimthOffset;
    float elevationOffset;
    float theta = 0;
    Vector3 TrackPos;
    void Start()
    {
        radarData = radar.GetComponent<Radar_Advanced>();
        standbyPos = Refarence.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        azimthOffset = 0;
        elevationOffset = 0;

        if(enable){
            radar.SetActive(true);
            if(slaving){
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
            }
            trackingTarget = null;
            foreach(GameObject target in radarData.targets){
                if(Mathf.Abs(Vector3.Distance(target.transform.position,transform.position) - rangeGate) < 1000){
                    trackingTarget = target;
                    slaving = false;
                    tracking = true;
                    isLost = false;
                    azimthOffset = 0;
                    elevationOffset = 0;
                }
                Debug.Log(Mathf.Abs(Vector3.Distance(target.transform.position,transform.position) - rangeGate));
            }
            if(tracking && trackingTarget != null){
                designatePos = trackingTarget.transform.position;
                Refarence.rotation = Quaternion.Slerp(Refarence.rotation,Quaternion.LookRotation(designatePos - transform.position),5f * Time.deltaTime);
                rangeGate = Vector3.Distance(designatePos,transform.position);
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
            if(cScanAxis != null){
                cScanAxis.transform.localEulerAngles += new Vector3(0,0,1800 * Time.deltaTime);
            }
        } else {
            trackingTarget = null;
            slaving = false;
            isLost = false;
            tracking = false;
            transform.rotation = Quaternion.Slerp(transform.rotation,standbyPos,1f * Time.deltaTime);
            radar.SetActive(false);
        }
        missiles.Clear();
        lastTracking = tracking;
        theta += 180 * Time.deltaTime;

        Azimth.transform.localEulerAngles = new Vector3(0,Refarence.localEulerAngles.y + azimthOffset,0);
        Elevation.transform.localEulerAngles = new Vector3(Refarence.localEulerAngles.x + elevationOffset,0,0);
    }
}
