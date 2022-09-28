using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawn : MonoBehaviour{
    public Transform target, bg1, bg2;
    private float size;

    void Start(){
        size = bg1.GetComponent<Renderer>().bounds.size.y*2;
    }
    void FixedUpdate(){
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = targetPos;

        if(transform.position.y >= bg2.position.y){
            bg1.position = new Vector3(bg1.position.x, bg2.position.y + size/2, bg1.position.z);
            SwitchBg();
        }
        else if(transform.position.y < bg2.position.y){
            bg1.position = new Vector3(bg1.position.x, bg2.position.y - size/2, bg1.position.z);
            SwitchBg();
        }
    }
    private void SwitchBg(){
        Transform temp = bg1;
        bg1 = bg2;
        bg2 = temp;
    }
}
