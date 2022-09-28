using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsCollide : MonoBehaviour
{
    public GameObject go1;
    private void OnTriggerStay(Collider other){
        if(other.gameObject.name == "Ball"){
            Debug.Log(this.gameObject.tag);
            if(this.gameObject.tag != "xPota" && ballThrow.m_Rigidbody.velocity == Vector3.zero){ //topun pota harici bir yerde stuck olma durumu
                if(RandomSpawner.goList.Count > 0){
                    StartCoroutine(ExecuteAfterTime(1f));
                }
            }
        }
    }
    IEnumerator ExecuteAfterTime(float time){
        yield return new WaitForSeconds(time);
        var ballPos = RandomSpawner.goList[RandomSpawner.goList.Count-2].transform.position;
        ballPos.y -= 0.03f;
        go1.transform.position = ballPos;
    }
}
