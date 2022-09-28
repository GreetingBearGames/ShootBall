using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetect : MonoBehaviour{
    public static bool collideDetect = false;
    public static GameObject go;
    public GameObject go1;
    public AudioSource ballBounceSE;
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "Ball"){
            if(RimController.isBallOnAir){
                ballBounceSE.Play();
                if(RandomSpawner.ballCollidedToUpperRim)
                    RandomSpawner.timeToMoveRim = false;
            }
            /*if(this.gameObject.tag != "xPota" && ballThrow.m_Rigidbody.velocity == Vector3.zero){ //topun pota harici bir yerde stuck olma durumu
                if(RandomSpawner.goList.Count > 0){
                    var ballPos = RandomSpawner.goList[RandomSpawner.goList.Count-2].transform.position;
                    ballPos.y -= 0.03f;
                    go1.transform.position = ballPos;
                }
            }*/
                
            go = this.gameObject;
            if(RandomSpawner.i == 1)
                BallPositioner2();
            else if(RandomSpawner.i == 2)
                BallPositioner1();
            else
                BallPositioner3();
            ballThrow.m_Rigidbody.velocity = Vector3.zero;
            ballThrow.m_Rigidbody.angularVelocity = Vector3.zero;
            if(collideDetect == false){
                collideDetect = true;
            }
        }
    }
    public void BallPositioner1(){
        if(Mathf.Abs(this.transform.position.x - RandomSpawner.goList[RandomSpawner.i-2].transform.position.x) < 0.5f &&
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-2].transform.position.y < 0.4f && 
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-2].transform.position.y > 0f){
            var ballPos = RandomSpawner.goList[RandomSpawner.i-2].transform.position;
            ballPos.y -= 0.03f;
            go1.transform.position = ballPos;
        }
        else if(Mathf.Abs(this.transform.position.x - RandomSpawner.goList[RandomSpawner.i-1].transform.position.x) < 0.5f &&
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-1].transform.position.y < 0.4f && 
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-1].transform.position.y > 0f){
            var ballPos = RandomSpawner.goList[RandomSpawner.i-1].transform.position;
            ballPos.y -= 0.03f;
            go1.transform.position = ballPos;
        }
        /*if(RimController.isBallOnAir)
            ballBounceSE.Play();*/
    }
    public void BallPositioner2(){
        if(Mathf.Abs(this.transform.position.x - RandomSpawner.goList[RandomSpawner.i-1].transform.position.x) < 0.5f &&
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-1].transform.position.y < 0.4f && 
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-1].transform.position.y > 0f){
            var ballPos = RandomSpawner.goList[RandomSpawner.i-1].transform.position;
            ballPos.y -= 0.03f;
            go1.transform.position = ballPos;
        }
        /*if(RimController.isBallOnAir)
            ballBounceSE.Play();*/
    }
    public void BallPositioner3(){
        if(Mathf.Abs(this.transform.position.x - RandomSpawner.goList[RandomSpawner.i-2].transform.position.x) < 0.5f &&
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-2].transform.position.y < 0.4f && 
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-2].transform.position.y > 0f){
            var ballPos = RandomSpawner.goList[RandomSpawner.i-2].transform.position;
            ballPos.y -= 0.03f;
            go1.transform.position = ballPos;
        }
        else if(Mathf.Abs(this.transform.position.x - RandomSpawner.goList[RandomSpawner.i-1].transform.position.x) < 0.5f &&
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-1].transform.position.y < 0.4f && 
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-1].transform.position.y > 0f){
            var ballPos = RandomSpawner.goList[RandomSpawner.i-1].transform.position;
            ballPos.y -= 0.03f;
            go1.transform.position = ballPos;
        }
        else if(Mathf.Abs(this.transform.position.x - RandomSpawner.goList[RandomSpawner.i-3].transform.position.x) < 0.5f &&
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-3].transform.position.y < 0.4f && 
            go1.transform.position.y - RandomSpawner.goList[RandomSpawner.i-3].transform.position.y > 0f){
            var ballPos = RandomSpawner.goList[RandomSpawner.i-3].transform.position;
            ballPos.y -= 0.03f;
            go1.transform.position = ballPos;
        }
        /*if(RimController.isBallOnAir)
            ballBounceSE.Play();*/
    }
}
