using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawner : MonoBehaviour
{
    public Vector3 center, size;
    public GameObject go,plane,plane1,go2,go3,go4;
    public List<GameObject> rimGoList;
    public int count, rimGoListSize;
    public static float currentHeight;
    public float previousHeight;
    public float rimRadius, obstacleSizeXaxis;
    public static int i;
    public static List<GameObject> goList;
    public float duration;
    public static float ballSize;
    public static bool timeToMoveRim, ballCollidedToUpperRim;

    void Start(){
        Score.currentScore = 0;
        rimGoList = new List<GameObject>();
        ballThrow.m_Thrust = 2f;
        timeToMoveRim = false;
        rimGoListSize = 1;
        count = 0;
        currentHeight = 0f;
        previousHeight = 0f;
        rimRadius = 1.54f;
        var m_collider = GameObject.FindWithTag("sepet").transform.GetChild(1).GetComponent<Collider>();
        rimRadius = m_collider.bounds.size.x/2;
        m_collider = GameObject.FindWithTag("obstacle").GetComponent<Collider>();
        obstacleSizeXaxis = m_collider.bounds.size.x/2;
        m_collider = GameObject.FindWithTag("Ball").GetComponent<Collider>();
        ballSize = m_collider.bounds.size.x;
        i=1;
        goList = new List<GameObject>();
        FirstRimSpawn();
        plane.transform.position = new Vector3(center.x-(size.x/2), plane.transform.position.y, 0);
        plane1.transform.position = new Vector3(center.x+(size.x/2), plane1.transform.position.y, 0);
    }
    void Update(){
        currentHeight = GameObject.FindGameObjectWithTag("Ball").transform.position.y;
        if(collisionDetect.collideDetect){
            collisionDetect.collideDetect = false;
            if(currentHeight > (previousHeight + 1.7f)){
                SpawnRim();
                ballCollidedToUpperRim = true;
                previousHeight = currentHeight;
                if(i > 2){
                    Destroy(goList[i-3]);
                    plane.transform.position = new Vector3(center.x-size.x/2, plane.transform.position.y + 3.0f, 0);
                    plane1.transform.position = new Vector3(center.x+size.x/2, plane1.transform.position.y + 3.0f, 0);                
                }
                else if(i > 1){
                    ballThrow.m_Thrust = 1.7f;
                }
                i++;
            }
            else{
                ballCollidedToUpperRim = false;
            }
        }
    }
    public void SpawnRim(){
        
        if(Score.currentScore == 6){
            rimGoList.Add(go2);
            rimGoList.Add(go3);
            rimGoListSize = 3;
        }
        else if(Score.currentScore == 12){
            rimGoList.Add(go4);
            rimGoListSize = 4;
        }
        if(Score.currentScore > 18  && Random.Range(0.0f, 2.0f) < 1.0f)
            timeToMoveRim = true;
        

        //// TEST PURPOSE ////
        ////              ////
        /*if(Score.currentScore == 3){
            rimGoList.Add(go2);
            rimGoList.Add(go3);
            rimGoListSize = 3;
        }
        else if(Score.currentScore == 6){
            rimGoList.Add(go4);
            rimGoListSize = 4;
        }
        if(Score.currentScore > 9  && Random.Range(0.0f, 2.0f) < 1.0f)
            timeToMoveRim = true;*/
        ////              ////
        //// TEST PURPOSE ////
        var randomPicker = Random.Range(0,rimGoListSize);
        var chosenRim = rimGoList[randomPicker];
        var numOfChild = chosenRim.transform.hierarchyCount - 1;
        Vector3 pos;
        if(numOfChild > 2){
            pos = center + new Vector3(Random.Range(-size.x/2+rimRadius + obstacleSizeXaxis, size.x/2-rimRadius-obstacleSizeXaxis),
            count, 0);
            if(pos.x - center.x + size.x/2 + rimRadius < ballSize){
                pos.x = center.x - size.x/2 + rimRadius*2;
            }
            else if(center.x + size.x/2 - pos.x - rimRadius < ballSize){
                pos.x = center.x + size.x/2 - rimRadius*2;
            }
        }
        else{
            pos = center + new Vector3(Random.Range(-size.x/2+rimRadius , size.x/2-rimRadius),
            count, 0);
            if(pos.x - center.x + size.x/2 + rimRadius < ballSize){
                pos.x = center.x - size.x/2 + rimRadius*2;
            }
            else if(center.x + size.x/2 - pos.x - rimRadius < ballSize){
                pos.x = center.x + size.x/2 - rimRadius*2;
            }
        }
        

        goList.Add((GameObject)Instantiate(chosenRim, pos, Quaternion.identity));
        count+=3;
    }
    public void FirstRimSpawn(){
        Vector3 pos = center + new Vector3(0,
        1, 0);
        rimGoList.Insert(0, go);
        goList.Add((GameObject)Instantiate(rimGoList[0], pos, Quaternion.identity));
        count+=4;
        collisionDetect.collideDetect = false;
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(center, size);
    }
}
