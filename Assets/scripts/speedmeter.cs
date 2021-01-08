using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedmeter : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 lastpos;
    void Start()
    {
        lastpos = new Vector3(0f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        var speed = (transform.position - lastpos) / Time.deltaTime;
        Debug.Log(speed.magnitude);
        lastpos = transform.position;
    }
}
