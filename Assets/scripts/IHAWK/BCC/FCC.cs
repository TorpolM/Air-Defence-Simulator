using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCC : MonoBehaviour
{
    public TCC _tcc;
    public HPIR HPIR_A;
    public RectTransform FSARepeatBack;
    public RectTransform FSARepeatBackRange;
    public RectTransform AltMeter;
    public RectTransform SigStr;
    public GameObject azimuthWheel;
    public RectTransform azimuthCursor;
    public RectTransform trackSymbol;
    public ButtonSwitch Btn_BreakLock;
    public bool isBreakLock;
     public float scale;
    // Start is called before the first frame update
    void Start()
    {
        
    }
         
    // Update is called once per frame
    void Update()
    {
        azimuthCursor.transform.localEulerAngles = new Vector3(0f,0f,azimuthWheel.GetComponent<handwheel>().position);
        var azimth = (azimuthWheel.GetComponent<handwheel>().position % 360);
        if(azimth < 0){
            azimth += 360;
        }


        trackSymbol.transform.localPosition = _tcc.trackSymbol.transform.localPosition * (_tcc.scale / scale);


        FSARepeatBack.transform.localEulerAngles = new Vector3(0f,0f,HPIR_A.Refarence.localEulerAngles.y);
        FSARepeatBackRange.transform.localPosition = new Vector3(0f,Mathf.Lerp(FSARepeatBackRange.transform.localPosition.y,HPIR_A.rangeGate / scale * 512,3.2f * Time.deltaTime),0);


        AltMeter.transform.localPosition = new Vector3(AltMeter.transform.localPosition.x,-230 + 90 * (HPIR_A.targetAlt / 20000),AltMeter.transform.localPosition.z);
        AltMeter.sizeDelta = new Vector2(4,190 * (HPIR_A.targetAlt / 20000));

        SigStr.transform.localPosition = new Vector3(-87 + 184 * Mathf.InverseLerp(100,0,-HPIR_A.targetStr),SigStr.transform.localPosition.y,SigStr.transform.localPosition.z);


        isBreakLock = Btn_BreakLock.pushed;
    }
}
