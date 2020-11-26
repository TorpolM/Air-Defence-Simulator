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
        var textSize = 0;
        if(mouse.tooltipText != null){
            textSize = mouse.tooltipText.Length * 10;
            tooltipTex.text = mouse.tooltipText;
            tooltipTex.rectTransform.sizeDelta = new Vector2(textSize,24f);
            tooltipImage.GetComponent<RectTransform>().sizeDelta = new Vector2(textSize,32f);
            tooltipImage.SetActive(true);
        } else {
            tooltipImage.SetActive(false);
        }
        
        var mousePos = Input.mousePosition;
        var magnification = canvasRect.sizeDelta.x / Screen.width;
        mousePos.x = mousePos.x * magnification - canvasRect.sizeDelta.x / 2 + textSize / 2 + 32;
        mousePos.y = mousePos.y * magnification - canvasRect.sizeDelta.y / 2 - 32;
        mousePos.z = transform.localPosition.z;

        tooltipImage.transform.localPosition = mousePos;
    }
}
