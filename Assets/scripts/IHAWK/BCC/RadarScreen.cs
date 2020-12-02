using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarScreen : MonoBehaviour
{
    bool isTCC;
    public GameObject TCC;
    TCC _TCC;
    public GameObject antenna;
    public GameObject beam;
    Radar_Advanced _beam;
    public GameObject antennaCW;
    public GameObject beamCW;
    Radar_Advanced _beamCW;
    public GameObject sweepLine;
    public GameObject rangeRings;
    public GameObject blip;
    public GameObject blipCW;
    public GameObject IFF;
    public GameObject SweepControl;
    handwheel _SweepControl;
    public GameObject IFFControl;
    handwheel _IFFControl;
    public GameObject VideoControl;
    handwheel _VideoControl;
    public GameObject RRControl;
    handwheel _RRControl;
    public float scale;
    public AnimationCurve curve;
    public int displayMode;
    List<RadarData> tgtData = new List<RadarData>();
    RadarData tgt;
    void Start()
    {
        _TCC = TCC.GetComponent<TCC>();
        _beam = beam.GetComponent<Radar_Advanced>();
        _beamCW = beamCW.GetComponent<Radar_Advanced>();
        _SweepControl = SweepControl.GetComponent<handwheel>();
        _RRControl = RRControl.GetComponent<handwheel>();
        _VideoControl = VideoControl.GetComponent<handwheel>();
        _IFFControl = IFFControl.GetComponent<handwheel>();
    }

    // Update is called once per frame
    void Update()
    {
        sweepLine.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
        sweepLine.transform.GetChild(0).GetComponent<Image>().color = new Color(0,255,0,_SweepControl.position/255);

        if(isTCC){
            for(int cnt = 0;cnt < 10;cnt++){
                rangeRings.transform.GetChild(cnt).GetComponent<Image>().color = new Color(0,255,0,_RRControl.position/255);
            }
        } else {
            for(int cnt = 0;cnt < 8;cnt++){
                rangeRings.transform.GetChild(cnt).GetComponent<Image>().color = new Color(0,255,0,_RRControl.position/255);
            }
        }

        

        //PPI-MTI
        tgtData = _beam.targetData;
        for(int cnt2 = 0;cnt2 < tgtData.Count;cnt2++){
            tgt = tgtData[cnt2];
            var range = Vector3.Distance(tgt.position,antenna.transform.position);
            GameObject echo = Instantiate(blip);
            echo.transform.parent = transform;
		    var PrdB = tgt.SignalStr;
            var size = Mathf.InverseLerp(100,20,-PrdB) + 1;
            var pos = tgt.position - antenna.transform.position;
            echo.GetComponent<BlipControl>().intencity = _VideoControl.position/255;
            echo.transform.localScale = new Vector3(range / scale * 2 + 1 * size,1*size,pos.y);
            echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
            if(pos.x / scale * 512f < 440f && pos.z / scale * 512f < 440f){
                echo.transform.localPosition = new Vector3(pos.x / scale * -512f,pos.z / scale * 512f,0f);
            }


            if((_TCC.isIFFAuto || _TCC.isIFFSend) && isTCC){

                if(_TCC.isIFFCoded && tgt.IFF == 1){
                    echo = Instantiate(IFF);
                    echo.transform.parent = transform;
                    pos = Vector3.Normalize(pos) * (range + 1280);
                    echo.GetComponent<BlipControl>().intencity = _IFFControl.position/255;
                    echo.transform.localScale = new Vector3(range / scale * 2 + 1 * size,1,pos.y);
                    echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
                    if(pos.x / scale * 512f < 440f && pos.z / scale * 512f < 440f){
                        echo.transform.localPosition = new Vector3(pos.x / scale * -512f,pos.z / scale * 512f,0f);
                    }
                }
                if(!_TCC.isIFFCoded && (tgt.IFF == 1 || tgt.IFF == 3)){
                    echo = Instantiate(IFF);
                    echo.transform.parent = transform;
                    pos = Vector3.Normalize(pos) * (range + 1280);
                    echo.GetComponent<BlipControl>().intencity = _IFFControl.position/255;
                    echo.transform.localScale = new Vector3(range / scale * 2 + 1 * size,1,pos.y);
                    echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
                    if(pos.x / scale * 512f < 440f && pos.z / scale * 512f < 440f){
                        echo.transform.localPosition = new Vector3(pos.x / scale * -512f,pos.z / scale * 512f,0f);
                    }
                }
            }
            

        }

        //PSI
        tgtData = _beamCW.targetData;
        for(int cnt2 = 0;cnt2 <  tgtData.Count;cnt2++){
             tgt = tgtData[cnt2];
            GameObject echo = Instantiate(blipCW);
            echo.transform.parent = transform;
		    var PrdB = tgt;
            var spd = Mathf.Cos(Vector3.Angle(antennaCW.transform.forward,tgt.forward) * Mathf.Deg2Rad) * -tgt.velocity.magnitude;
            spd = spd / 1234 * 75;
            var pos = Vector3.Normalize(tgt.position - antenna.transform.position);
            echo.transform.GetComponent<Image>().color = new Color(0,255,0,VideoControl.GetComponent<handwheel>().position/255);
            pos = pos * (494f - spd);
            echo.transform.localScale = new Vector3(1,1,1);
            echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
            if(true){
                echo.transform.localPosition = new Vector3(-pos.x,pos.z,0f);;
            }
        }
    }


    public static float CurveWeightedRandom(AnimationCurve curve)
    {
        return curve.Evaluate(Random.value);
    }
}