using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TCC : MonoBehaviour
{
    public GameObject azimuthWheel;
    public RectTransform azimuthCursor;
    public Text azimuthMils;
    public GameObject lever;
    public RectTransform trackSymbol;
    public IndicatorButton Btn_IFF1;
    public IndicatorButton Btn_IFF2;
    public IndicatorButton Btn_IFF3;
    public IndicatorButton Btn_IFF4;
    public int IFFMode = 1;
    public IndicatorButton Btn_IFFAuto;
    public bool isIFFAuto;
    public IndicatorButton Btn_IFFSend;
    public bool isIFFSend;
    public IndicatorButton Btn_IFFCoded;
    public bool isIFFCoded;
    public IndicatorButton Btn_IDHost;
    public bool isIDHost;
    public IndicatorButton Btn_IDFrnd;
    public bool isIDFrnd;
    public IndicatorButton Btn_IDUnk;
    public bool isIDUnk;
    public IndicatorButton Btn_DispLocal;
    public bool DispLocal = true;
    public IndicatorButton Btn_DispRemote;
    public bool DispRemote = true;
    public IndicatorButton Btn_DispFrnd;
    public bool DispFrnd = true;
    public IndicatorButton Btn_DispHost;
    public bool DispHost = true;
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
        azimuthMils.text = Mathf.FloorToInt(azimth * Mathf.Deg2Rad * 1000).ToString("0000");


        var leverPos = lever.GetComponent<DesignatorLever>();
        trackSymbol.transform.localPosition += new Vector3(leverPos.moveX * Time.deltaTime * -20f,leverPos.moveY * Time.deltaTime * 20f,0f);


        if(Btn_IFF1.pushed){
            IFFMode = 1;
        }
        if(Btn_IFF2.pushed){
            IFFMode = 2;
        }
        if(Btn_IFF3.pushed){
            IFFMode = 3;
        }
        if(Btn_IFF4.pushed){
            IFFMode = 4;
        }
        if(IFFMode == 1){
            Btn_IFF1.lampOn = true;
        } else {
            Btn_IFF1.lampOn = false;
        }
        if(IFFMode == 2){
            Btn_IFF2.lampOn = true;
        } else {
            Btn_IFF2.lampOn = false;
        }
        if(IFFMode == 3){
            Btn_IFF3.lampOn = true;
        } else {
            Btn_IFF3.lampOn = false;
        }
        if(IFFMode == 4){
            Btn_IFF4.lampOn = true;
        } else {
            Btn_IFF4.lampOn = false;
        }


        isIFFSend = Btn_IFFSend.pushed;


        if(Btn_IFFCoded.pushed){
            if(isIFFCoded){
                isIFFCoded = false;
            } else {
                isIFFCoded = true;
            }
        }
        Btn_IFFCoded.lampOn = isIFFCoded;


        if(Btn_IFFAuto.pushed){
            if(isIFFAuto){
                isIFFAuto = false;
            } else {
                isIFFAuto = true;
            }
        }
        Btn_IFFAuto.lampOn = !isIFFAuto;

        isIDHost = Btn_IDHost.pushed;
        isIDFrnd = Btn_IDFrnd.pushed;
        isIDUnk = Btn_IDUnk.pushed;


        if(Btn_DispLocal.pushed){
            if(DispLocal){
                DispLocal = false;
            } else {
                DispLocal = true;
            }
        }
        Btn_DispLocal.lampOn = DispLocal;
        if(Btn_DispRemote.pushed){
            if(DispRemote){
                DispRemote = false;
            } else {
                DispRemote = true;
            }
        }
        Btn_DispRemote.lampOn = DispRemote;
        if(Btn_DispFrnd.pushed){
            if(DispFrnd){
                DispFrnd = false;
            } else {
                DispFrnd = true;
            }
        }
        Btn_DispFrnd.lampOn = DispFrnd;
        if(Btn_DispHost.pushed){
            if(DispHost){
                DispHost = false;
            } else {
                DispHost = true;
            }
        }
        Btn_DispHost.lampOn = DispHost;
    }
}
