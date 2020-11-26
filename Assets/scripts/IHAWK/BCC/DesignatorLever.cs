using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignatorLever : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject lever;
    bool isGrasped = false;
    public float moveX;
    public float moveY;
    public string name;
    void Start()
    {
        lever = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrasped){
            moveX =  Input.GetAxis("Mouse X");
            moveY =  Input.GetAxis("Mouse Y");
            if(Input.GetKeyUp(KeyCode.Mouse0)){
                isGrasped = false;
                Cursor.lockState = CursorLockMode.None;
            }
            lever.transform.localEulerAngles = new Vector3(moveY * 3f,0f,moveX * -3f);
        }
    }

    public void grasp(){
        Cursor.lockState = CursorLockMode.Locked;
        isGrasped = true;
    }
}
