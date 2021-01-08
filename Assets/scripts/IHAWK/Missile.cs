using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    GameObject tgt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void onFire(){
        tgt = transform.GetChild(2).gameObject;
        transform.GetChild(2).parent = null;

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);

        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
    }
}
