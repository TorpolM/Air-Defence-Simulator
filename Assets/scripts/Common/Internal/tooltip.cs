using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tooltip : MonoBehaviour
{
    // Start is called before the first frame update
    public MouseConsoleControl mouse;
    public GameObject tooltipImage;
    Text tooltipTex;
    RectTransform canvasRect;

    void Start()
    {
        tooltipTex = tooltipImage.transform.GetChild(0).GetComponent<Text>();
        canvasRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mouse.tooltipText != null){
            tooltipTex.text = mouse.tooltipText;
            tooltipImage.SetActive(true);
        } else {
            tooltipImage.SetActive(false);
        }
        
        var mousePos = Input.mousePosition;
        var magnification = canvasRect.sizeDelta.x / Screen.width;
        mousePos.x = mousePos.x * magnification - canvasRect.sizeDelta.x / 2 + 100;
        mousePos.y = mousePos.y * magnification - canvasRect.sizeDelta.y / 2 - 32;
        mousePos.z = transform.localPosition.z;

        tooltipImage.transform.localPosition = mousePos;
    }
}
