using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseConsoleControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera Cam;
    public GameObject obj;

    public GameObject[] shortCut;   //0~9,BS
    public GameObject shortCutEnter;
    public GameObject shortCutSpace;
    public GameObject shortCutBS;
    public Texture2D mouseIcon; 

    KeyCode[] keys= {KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9,KeyCode.Backspace};
    GameObject interestObj;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Camera mainCamera = Cam;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit)){
            Collider[] hitColliders = Physics.OverlapSphere(hit.point, 0.001f);
            hitColliders = new Collider[1];
            hitColliders[0] = hit.collider;
            if (hitColliders.Length > 0){
                interestObj =  hitColliders[0].gameObject;
                if (interestObj.GetComponent<DialSwitch>() != null){
                    if (Input.GetKeyDown(KeyCode.Mouse0) && interestObj.GetComponent<DialSwitch>().position > 0){
                        interestObj.GetComponent<DialSwitch>().SoundCW();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && interestObj.GetComponent<DialSwitch>().position < (interestObj.GetComponent<DialSwitch>().numPosition - 1)){
                        interestObj.GetComponent<DialSwitch>().SoundCCW();
                    }
                }
                if (interestObj.GetComponent<Switch>() != null){
                    if (Input.GetKeyDown(KeyCode.Mouse0) && interestObj.GetComponent<Switch>().position > 0){
                        interestObj.GetComponent<Switch>().SoundNx();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && interestObj.GetComponent<Switch>().position < (interestObj.GetComponent<Switch>().numPosition - 1)){
                        interestObj.GetComponent<Switch>().SoundPv();
                    }
                }
                if (interestObj.GetComponent<DesignatorLever>() != null){
                    if (Input.GetKeyDown(KeyCode.Mouse0)){
                        interestObj.GetComponent<DesignatorLever>().grasp();
                    }
                }
                if (interestObj.GetComponent<handwheel>() != null){
                    if (Input.GetKey(KeyCode.Mouse0)){
                        interestObj.GetComponent<handwheel>().SoundCW();
                    }
                    if (Input.GetKey(KeyCode.Mouse1)){
                        interestObj.GetComponent<handwheel>().SoundCCW();
                    }
                }
                if (interestObj.GetComponent<ButtonSwitch>() != null){
                    if (Input.GetKey(KeyCode.Mouse0)){
                        interestObj.GetComponent<ButtonSwitch>().pushed = true;  
                    }
                }
                if (interestObj.GetComponent<IndicatorButton>() != null){
                    if(interestObj.GetComponent<IndicatorButton>().pushInstant){
                        if (Input.GetKeyDown(KeyCode.Mouse0)){
                            interestObj.GetComponent<IndicatorButton>().pushed = true;  
                        }
                    } else {
                        if (Input.GetKey(KeyCode.Mouse0)){
                            interestObj.GetComponent<IndicatorButton>().pushed = true;  
                        }
                    }
                }
                if (interestObj.GetComponent<Station>() != null){
                    if (Input.GetKey(KeyCode.Mouse0)){
                        GetComponent<InternalCam>().changeStation(interestObj);
                    }
                }
                //Vector2 hotspot = mouseIcon.texelSize * 0.5f;
                //hotspot.y *= -1;
                //Cursor.SetCursor(null,hotspot,CursorMode.Auto);
            }
        }
        if(Cam.GetComponent<InternalCam>().crtStation != null){
            shortCut = Cam.GetComponent<InternalCam>().crtStation.GetComponent<Station>().shortCut;
            for(int cnt = 0;cnt < shortCut.Length;cnt++){
                interestObj = shortCut[cnt];
                if (interestObj.GetComponent<DialSwitch>() != null){
                    if (Input.GetKeyDown(KeyCode.Mouse0) && interestObj.GetComponent<DialSwitch>().position > 0){
                        interestObj.GetComponent<DialSwitch>().position += -1;
                        interestObj.GetComponent<DialSwitch>().SoundCW();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && interestObj.GetComponent<DialSwitch>().position < (interestObj.GetComponent<DialSwitch>().numPosition - 1)){
                        interestObj.GetComponent<DialSwitch>().position += +1;
                        interestObj.GetComponent<DialSwitch>().SoundCCW();
                    }
                }
                if (interestObj.GetComponent<Switch>() != null){
                    if (Input.GetKeyDown(KeyCode.Mouse0) && interestObj.GetComponent<Switch>().position > 0){
                        interestObj.GetComponent<Switch>().position += -1;
                        interestObj.GetComponent<Switch>().SoundNx();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && interestObj.GetComponent<Switch>().position < (interestObj.GetComponent<Switch>().numPosition - 1)){
                        interestObj.GetComponent<Switch>().position += +1;
                        interestObj.GetComponent<Switch>().SoundPv();
                    }
                }
                if (interestObj.GetComponent<ButtonSwitch>() != null){
                    if (Input.GetKey(keys[cnt])){
                        interestObj.GetComponent<ButtonSwitch>().pushed = true;  
                    }
                }
            }
            if(Cam.GetComponent<InternalCam>().crtStation.GetComponent<Station>().shortCutBS != null){
                interestObj = Cam.GetComponent<InternalCam>().crtStation.GetComponent<Station>().shortCutBS;
                if (interestObj.GetComponent<DialSwitch>() != null){
                    if (Input.GetKeyDown(KeyCode.Mouse0) && interestObj.GetComponent<DialSwitch>().position > 0){
                        interestObj.GetComponent<DialSwitch>().position += -1;
                        interestObj.GetComponent<DialSwitch>().SoundCW();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && interestObj.GetComponent<DialSwitch>().position < (interestObj.GetComponent<DialSwitch>().numPosition - 1)){
                        interestObj.GetComponent<DialSwitch>().position += +1;
                        interestObj.GetComponent<DialSwitch>().SoundCCW();
                    }
                }
                if (interestObj.GetComponent<Switch>() != null){
                    if (Input.GetKeyDown(KeyCode.Mouse0) && interestObj.GetComponent<Switch>().position > 0){
                        interestObj.GetComponent<Switch>().position += -1;
                        interestObj.GetComponent<Switch>().SoundNx();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && interestObj.GetComponent<Switch>().position < (interestObj.GetComponent<Switch>().numPosition - 1)){
                        interestObj.GetComponent<Switch>().position += +1;
                        interestObj.GetComponent<Switch>().SoundPv();
                    }
                }
                if (interestObj.GetComponent<ButtonSwitch>() != null){
                    if (Input.GetKey(KeyCode.Backspace)){
                        interestObj.GetComponent<ButtonSwitch>().pushed = true;  
                    }
                }
            }
            if(Cam.GetComponent<InternalCam>().crtStation.GetComponent<Station>().shortCutEnter != null){
                interestObj = Cam.GetComponent<InternalCam>().crtStation.GetComponent<Station>().shortCutEnter;
                if (interestObj.GetComponent<DialSwitch>() != null){
                    if (Input.GetKeyDown(KeyCode.Mouse0) && interestObj.GetComponent<DialSwitch>().position > 0){
                        interestObj.GetComponent<DialSwitch>().position += -1;
                        interestObj.GetComponent<DialSwitch>().SoundCW();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && interestObj.GetComponent<DialSwitch>().position < (interestObj.GetComponent<DialSwitch>().numPosition - 1)){
                        interestObj.GetComponent<DialSwitch>().position += +1;
                        interestObj.GetComponent<DialSwitch>().SoundCCW();
                    }
                }
                if (interestObj.GetComponent<Switch>() != null){
                    if (Input.GetKeyDown(KeyCode.Mouse0) && interestObj.GetComponent<Switch>().position > 0){
                        interestObj.GetComponent<Switch>().position += -1;
                        interestObj.GetComponent<Switch>().SoundNx();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && interestObj.GetComponent<Switch>().position < (interestObj.GetComponent<Switch>().numPosition - 1)){
                        interestObj.GetComponent<Switch>().position += +1;
                        interestObj.GetComponent<Switch>().SoundPv();
                    }
                }
                if (interestObj.GetComponent<ButtonSwitch>() != null){
                    if (Input.GetKey(KeyCode.Return)){
                        interestObj.GetComponent<ButtonSwitch>().pushed = true;  
                    }
                }
            }
            if(Cam.GetComponent<InternalCam>().crtStation.GetComponent<Station>().shortCutSpace != null){
                interestObj = Cam.GetComponent<InternalCam>().crtStation.GetComponent<Station>().shortCutSpace;
                if (interestObj.GetComponent<DialSwitch>() != null){
                    if (Input.GetKeyDown(KeyCode.Mouse0) && interestObj.GetComponent<DialSwitch>().position > 0){
                        interestObj.GetComponent<DialSwitch>().position += -1;
                        interestObj.GetComponent<DialSwitch>().SoundCW();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && interestObj.GetComponent<DialSwitch>().position < (interestObj.GetComponent<DialSwitch>().numPosition - 1)){
                        interestObj.GetComponent<DialSwitch>().position += +1;
                        interestObj.GetComponent<DialSwitch>().SoundCCW();
                    }
                }
                if (interestObj.GetComponent<Switch>() != null){
                    if (Input.GetKeyDown(KeyCode.Mouse0) && interestObj.GetComponent<Switch>().position > 0){
                        interestObj.GetComponent<Switch>().position += -1;
                        interestObj.GetComponent<Switch>().SoundNx();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1) && interestObj.GetComponent<Switch>().position < (interestObj.GetComponent<Switch>().numPosition - 1)){
                        interestObj.GetComponent<Switch>().position += +1;
                        interestObj.GetComponent<Switch>().SoundPv();
                    }
                }
                if (interestObj.GetComponent<ButtonSwitch>() != null){
                    if (Input.GetKey(KeyCode.Space)){
                        interestObj.GetComponent<ButtonSwitch>().pushed = true;  
                    }
                }
            }
        }
    }
}