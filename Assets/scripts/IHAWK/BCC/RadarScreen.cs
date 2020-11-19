using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarScreen : MonoBehaviour
{
    public GameObject TCC;
    public GameObject antenna;
    public GameObject beam;
    public GameObject antennaCW;
    public GameObject beamCW;
    public GameObject sweepLine;
    public GameObject rangeRings;
    public GameObject blip;
    public GameObject blipCW;
    public GameObject IFF;
    public GameObject SweepControl;
    public GameObject IFFControl;
    public GameObject VideoControl;
    public GameObject RRControl;
    public float scale;
    public AnimationCurve curve;
    public int displayMode;

    List<GameObject> targets = new List<GameObject>();
    List<float> targetStrs = new List<float>();
    List<int> targetIFFs = new List<int>();

    public float Pt;
    public float dB;
    public float Ft;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sweepLine.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
        sweepLine.transform.GetChild(0).GetComponent<Image>().color = new Color(0,255,0,SweepControl.GetComponent<handwheel>().position/255);

        for(int cnt = 0;cnt < 10;cnt++){
            rangeRings.transform.GetChild(cnt).GetComponent<Image>().color = new Color(0,255,0,RRControl.GetComponent<handwheel>().position/255);
        }

        

        //PPI-MTI
        targets = beam.GetComponent<Radar_Advanced>().targets;
        targetStrs = beam.GetComponent<Radar_Advanced>().targetStrs;
        targetIFFs = beam.GetComponent<Radar_Advanced>().targetIFF;
        for(int cnt2 = 0;cnt2 < targets.Count;cnt2++){
            var target = targets[cnt2];
            GameObject echo = Instantiate(blip);
            echo.transform.parent = transform;
		    var PrdB = targetStrs[cnt2];
            var size = (136 + PrdB) / 10 /* -Mathf.Cos(Vector3.Angle(antenna.transform.forward,target.transform.forward))*/;
            var pos = target.transform.position - antenna.transform.position;
            echo.transform.GetComponent<Image>().color = new Color(0,255,0,VideoControl.GetComponent<handwheel>().position/255);
            echo.transform.localScale = new Vector3(size,1,pos.y);
            echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
            if(pos.x / scale * 512f < 420f && pos.z / scale * 512f < 420f){
                echo.transform.localPosition = new Vector3(pos.x / scale * -512f,pos.z / scale * 512f,0f);
            }
            Destroy(echo,size / 5);

            if(TCC.GetComponent<TCC>().isIFFAuto || TCC.GetComponent<TCC>().isIFFSend){
                var range = Vector3.Distance(target.transform.position,antenna.transform.position);
                if(TCC.GetComponent<TCC>().isIFFCoded && targetIFFs[cnt2] == 1){
                    echo = Instantiate(IFF);
                    echo.transform.parent = transform;
                    pos = pos * 1.01f;
                    echo.transform.GetComponent<Image>().color = new Color(0,255,0,VideoControl.GetComponent<handwheel>().position/255);
                    echo.transform.localScale = new Vector3(range/100000 * 5,1f,pos.y);
                    echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
                    if(pos.x / scale * 512f < 420f && pos.z / scale * 512f < 420f){
                        echo.transform.localPosition = new Vector3(pos.x / scale * -512f,pos.z / scale * 512f,0f);
                    }
                    Destroy(echo,size / 5);
                }
                if(!TCC.GetComponent<TCC>().isIFFCoded && (targetIFFs[cnt2] == 1 || targetIFFs[cnt2] == 3)){
                    echo = Instantiate(IFF);
                    echo.transform.parent = transform;
                    pos = pos * 1.032f;
                    echo.transform.GetComponent<Image>().color = new Color(0,255,0,VideoControl.GetComponent<handwheel>().position/255);
                    echo.transform.localScale = new Vector3(range/100000 * 5,1f,pos.y);
                    echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
                    if(pos.x / scale * 512f < 420f && pos.z / scale * 512f < 420f){
                        echo.transform.localPosition = new Vector3(pos.x / scale * -512f,pos.z / scale * 512f,0f);
                    }
                    Destroy(echo,size / 5);
                }
            }
            
        }

        


        //PSI
        targets = beamCW.GetComponent<Radar_Advanced>().targets;
        targetStrs = beamCW.GetComponent<Radar_Advanced>().targetStrs;
        for(int cnt2 = 0;cnt2 < targets.Count;cnt2++){
            var target = targets[cnt2];
            GameObject echo = Instantiate(blipCW);
            echo.transform.parent = transform;
		    var PrdB = targetStrs[cnt2];
            var size = (136 + PrdB) / 10;
            var spd = Mathf.Cos(Vector3.Angle(antennaCW.transform.forward,target.transform.forward) * Mathf.Deg2Rad) * -target.GetComponent<Rigidbody>().velocity.magnitude;
            spd = spd / 1234 * 75;
            var pos = Vector3.Normalize(target.transform.position - antenna.transform.position);
            echo.transform.GetComponent<Image>().color = new Color(0,255,0,VideoControl.GetComponent<handwheel>().position/255);
            pos = pos * (475.5f - spd);
            echo.transform.localScale = new Vector3(1,1,1);
            echo.transform.localEulerAngles = new Vector3(0f,0f,antenna.transform.eulerAngles.y);
            if(true){
                echo.transform.localPosition = new Vector3(-pos.x,pos.z,0f);;
            }
            Destroy(echo,size / 5);
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
