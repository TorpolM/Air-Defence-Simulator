using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCC : MonoBehaviour
{
    public TCC _tcc;
    public GameObject azimuthWheel;
    public RectTransform azimuthCursor;
    public RectTransform trackSymbol;
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

    }
}
