
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlipControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float intencity = 1;
    Image img;
    void Start()
    {
        img = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        intencity += -0.5f * Time.deltaTime;
        img.color = new Color(0,1,0,intencity);
        if(intencity < 0){
            Destroy(this.gameObject);
        }
    }
}
