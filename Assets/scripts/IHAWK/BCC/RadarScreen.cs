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

    List<GameObject> targets = new List<GameObject>();
    List<float> targetStrs = new List<float>();
    List<int> targetIFFs = new List<int>();
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
        targets = _beam.targets;
        targetStrs = _beam.targetStrs;
        targetIFFs = _beam.targetIFF;
        for(int cnt2 = 0;cnt2 < targets.Count;cnt2++){
            var target = targets[cnt2];
            var range = Vector3.Distance(target.transform.position,antenna.transform.position);
            GameObject echo = Instantiate(blip);
            echo.transform.parent = transform;
		    var PrdB = targetStrs[cnt2];
            var size = Mathf.InverseLerp(100,20,-PrdB) + 1;
            var pos = target.transform.position - antenna.transform.position;
            echo.GetComponent<BlipControl>().intencity = _VideoControl.position/255;
            echo.transform.localScale = new Vector3(range / scale * 2 + 1 * size,1*size,pos.y);
            echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
            if(pos.x / scale * 512f < 440f && pos.z / scale * 512f < 440f){
                echo.transform.localPosition = new Vector3(pos.x / scale * -512f,pos.z / scale * 512f,0f);
            }


            if((_TCC.isIFFAuto || _TCC.isIFFSend) && isTCC){

                if(_TCC.isIFFCoded && targetIFFs[cnt2] == 1){
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
                if(!_TCC.isIFFCoded && (targetIFFs[cnt2] == 1 || targetIFFs[cnt2] == 3)){
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
        targets = _beamCW.targets;
        targetStrs = _beamCW.targetStrs;
        for(int cnt2 = 0;cnt2 < targets.Count;cnt2++){
            var target = targets[cnt2];
            GameObject echo = Instantiate(blipCW);
            echo.transform.parent = transform;
		    var PrdB = targetStrs[cnt2];
            var spd = Mathf.Cos(Vector3.Angle(antennaCW.transform.forward,target.transform.forward) * Mathf.Deg2Rad) * -target.GetComponent<Rigidbody>().velocity.magnitude;
            spd = spd / 1234 * 75;
            var pos = Vector3.Normalize(target.transform.position - antenna.transform.position);
            echo.transform.GetComponent<Image>().color = new Color(0,255,0,VideoControl.GetComponent<handwheel>().position/255);
            pos = pos * (494f - spd);
            echo.transform.localScale = new Vector3(1,1,1);
            echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
            if(true){
                echo.transform.localPosition = new Vector3(-pos.x,pos.z,0f);;
            }
        }



        /*----Clutters
        for(int cnt = 0;cnt < Random.Range(2,8);cnt++){
            GameObject echo = Instantiate(blip);
            echo.transform.parent = transform;
            var size = CurveWeightedRandom(curve) * 0.24f + 0.01f;
            echo.transform.localScale = new Vector3(size,size,1f);
            echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
            var dist = CurveWeightedRandom(curve) * 512f;
            var angle = antenna.transform.eulerAngles.y-90f;
            echo.transform.localPosition = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * -dist,Mathf.Sin(angle * Mathf.Deg2Rad) * -dist,0f);
            Destroy(echo,size);
        }
        var rayPosition = antenna.transform.position; 
        Ray ray = new Ray(rayPosition,antenna.transform.forward); 
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,100000))
        {
            if (hit.collider.tag == "terrain") 
            {
                
                GameObject echo = Instantiate(blip);
                echo.transform.parent = transform;
                var pos = hit.point - antenna.transform.position;
                var size = CurveWeightedRandom(curve) * 0.64f + 0.12f;
                echo.transform.localScale = new Vector3(size,size,pos.y);
                echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
                if(pos.x / scale * 512f < 512f && pos.z / scale * 512f < 512f){
                    echo.transform.localPosition = new Vector3(pos.x / scale * -512f,pos.z / scale * 512f,0f);
                }
                Destroy(echo,size);
            }
        }*/
    }


    public static float CurveWeightedRandom(AnimationCurve curve)
    {
        return curve.Evaluate(Random.value);
    }
}
