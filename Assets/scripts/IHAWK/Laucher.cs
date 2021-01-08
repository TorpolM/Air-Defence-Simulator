using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laucher : MonoBehaviour
{
    public HPIR hpir;
    public bool selected;
    public Transform rail1;
    public Transform rail2;
    public Transform rail3;
    public GameObject missile1;
    public GameObject missile2;
    public GameObject missile3;
    public GameObject msl;
    void Start()
    {
        reload();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launch(){
        if(missile1 != null){
            missile1.GetComponent<Missile>().onFire();
            return;
        }
        if(missile2 != null){
            missile2.GetComponent<Missile>().onFire();
            return;
        }
        if(missile3 != null){
            missile3.GetComponent<Missile>().onFire();
            return;
        }
    }

    void reload(){
        GameObject missile1 = Instantiate(msl);
        missile1.transform.parent = rail1;
        missile1.transform.localEulerAngles = new Vector3(0,0,0);
        missile1.transform.localPosition = new Vector3(0,0,0);

        GameObject missile2 = Instantiate(msl);
        missile2.transform.parent = rail2;
        missile2.transform.localEulerAngles = new Vector3(0,0,0);
        missile2.transform.localPosition = new Vector3(0,0,0);

        GameObject missile3 = Instantiate(msl);
        missile3.transform.parent = rail3;
        missile3.transform.localEulerAngles = new Vector3(0,0,0);
        missile3.transform.localPosition = new Vector3(0,0,0);
        
    }
}
