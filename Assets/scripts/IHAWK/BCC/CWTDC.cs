using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWTDC : MonoBehaviour
{
    public GameObject antennaCW;
    public GameObject beamCW;
    Radar_Advanced _beamCW;
    public RectTransform sweep;
    public RectTransform cursor;
    public GameObject azimuthWheel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cursor.transform.localPosition = new Vector3((azimuthWheel.GetComponent<handwheel>().position / 360) * 2700 - 1350,0,1e-05f);
        var azimth = (azimuthWheel.GetComponent<handwheel>().position % 360);
        if(azimth < 0){
            azimth += 360;
        }

        sweep.transform.localPosition = new Vector3((antennaCW.transform.localEulerAngles.y / 360) * 2700 - 1350,0,1e-05f);
    }
}
