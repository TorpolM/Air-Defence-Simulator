using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolControl : MonoBehaviour
{
    public GameObject tcc;
    TCC _tcc;
    public Sprite[] images;
    public GameObject truck;
    truckFile _truck;
    public Vector3 antennaPos;
    public float scale;
    public int ID;
    public int truckID;
    void Start()
    {
        _truck = truck.GetComponent<truckFile>();
        _tcc = tcc.GetComponent<TCC>();
    }

    // Update is called once per frame
    void Update()
    {
        if(truck != null){
            ID = _truck.ID;
            transform.GetComponent<Image>().sprite = images[ID];
            transform.localScale = new Vector3(1f,1f,1f);
            transform.localEulerAngles = new Vector3(0f,0f,0f);
            var pos = new Vector3(_truck.currentPos.x,0f,_truck.currentPos.y);
            pos = pos - antennaPos;
            transform.localPosition = new Vector3(pos.x / scale * -512f,pos.z / scale * 512f,0f);
        } else {
            Destroy(this.gameObject);
        }
    }
}
