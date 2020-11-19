using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolControl : MonoBehaviour
{
    public TCC tcc;
    public Sprite[] images;
    public GameObject truck;
    public Vector3 antennaPos;
    public float scale;
    public int ID;
    public int truckID;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(truck != null){
            ID = truck.GetComponent<truckFile>().ID;
            transform.GetComponent<Image>().sprite = images[ID];
            transform.localScale = new Vector3(1f,1f,1f);
            transform.localEulerAngles = new Vector3(0f,0f,0f);
            var pos = new Vector3(truck.GetComponent<truckFile>().currentPos.x,0f,truck.GetComponent<truckFile>().currentPos.y);
            pos = pos - antennaPos;
            transform.localPosition = new Vector3(pos.x / scale * -420f,pos.z / scale * 420f,0f);
        } else {
            Destroy(this.gameObject);
        }
    }
}
