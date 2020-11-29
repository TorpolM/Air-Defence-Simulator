using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truckFile:MonoBehaviour{
    public int fileID;
    public Vector2 truckPos;
    public Vector2 truckVector;
    public Vector2 currentPos;
    public float utime;
    public int ID = 0;  //0:UNK 1:FRND 2:HOST
    public bool isRemote;
    public int type = 0;//0:UNK 1:FRND 2:HOST 3:OSVAHost 4:PltHost 5:Assigned 6:tracked;


    void Start(){
        
    }
    void Update()
    {
        currentPos += truckVector * Time.deltaTime;
        if(Time.time - utime > 10){
            Destroy(this.gameObject);
        }
    }
    
    public void updateTruck(Vector2 newPos,float timeOffset){
        truckVector = (newPos - truckPos) / (Time.time - utime + 0.0000001f);
        truckPos = newPos;
        currentPos = truckPos + truckVector * timeOffset;
        utime = Time.time;
    }
}
