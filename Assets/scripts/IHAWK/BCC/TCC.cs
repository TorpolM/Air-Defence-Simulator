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
    public GameObject Btn_IFF1;
    public GameObject Btn_IFF2;
    public GameObject Btn_IFF3;
    public GameObject Btn_IFF4;
    public int IFFMode = 1;
    public GameObject Btn_IFFAuto;
    public bool isIFFAuto;
    public GameObject Btn_IFFSend;
    public bool isIFFSend;
    public GameObject Btn_IFFCoded;
    public bool isIFFCoded;
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


        if(Btn_IFF1.GetComponent<IndicatorButton>().pushed){
            IFFMode = 1;
        }
        if(Btn_IFF2.GetComponent<IndicatorButton>().pushed){
            IFFMode = 2;
        }
        if(Btn_IFF3.GetComponent<IndicatorButton>().pushed){
            IFFMode = 3;
        }
        if(Btn_IFF4.GetComponent<IndicatorButton>().pushed){
            IFFMode = 4;
        }
        if(IFFMode == 1){
            Btn_IFF1.GetComponent<IndicatorButton>().lampOn = true;
        } else {
            Btn_IFF1.GetComponent<IndicatorButton>().lampOn = false;
        }
        if(IFFMode == 2){
            Btn_IFF2.GetComponent<IndicatorButton>().lampOn = true;
        } else {
            Btn_IFF2.GetComponent<IndicatorButton>().lampOn = false;
        }
        if(IFFMode == 3){
            Btn_IFF3.GetComponent<IndicatorButton>().lampOn = true;
        } else {
            Btn_IFF3.GetComponent<IndicatorButton>().lampOn = false;
        }
        if(IFFMode == 4){
            Btn_IFF4.GetComponent<IndicatorButton>().lampOn = true;
        } else {
            Btn_IFF4.GetComponent<IndicatorButton>().lampOn = false;
        }


        isIFFSend = Btn_IFFSend.GetComponent<IndicatorButton>().pushed;


        if(Btn_IFFCoded.GetComponent<IndicatorButton>().pushed){
            if(isIFFCoded){
                isIFFCoded = false;
            } else {
                isIFFCoded = true;
            }
        }
        Btn_IFFCoded.GetComponent<IndicatorButton>().lampOn = isIFFCoded;


        if(Btn_IFFAuto.GetComponent<IndicatorButton>().pushed){
            if(isIFFAuto){
                isIFFAuto = false;
            } else {
                isIFFAuto = true;
            }
        }
        Btn_IFFAuto.GetComponent<IndicatorButton>().lampOn = !isIFFAuto;
    }
}
