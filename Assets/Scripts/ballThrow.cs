using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballThrow : MonoBehaviour
{
    
    public static Rigidbody m_Rigidbody;
    public static float m_Thrust;
    static Vector3 direction, direction1;
    public static bool isGameOver = false;
    public static Vector3 targetBallPos;
    [SerializeField] private GameObject can;
    void Start(){
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    void Update(){
        Renderer renderer = GetComponent<Renderer>();
        if(RandomSpawner.i > 2){
            if(transform.position.y < RandomSpawner.goList[RandomSpawner.i - 3].transform.position.y - 1.0f){
                isGameOver = true;
                can.transform.GetChild(3).gameObject.SetActive(true);
                m_Rigidbody.useGravity = false;
                m_Rigidbody.velocity = new Vector3(0,0,0);
                m_Rigidbody.freezeRotation = true;
            }
        }
        else if(RandomSpawner.i == 2){
            if(transform.position.y < RandomSpawner.goList[RandomSpawner.i - 2].transform.position.y - 1.0f){
                isGameOver = true;
                can.transform.GetChild(3).gameObject.SetActive(true);
                m_Rigidbody.useGravity = false;
                m_Rigidbody.velocity = new Vector3(0,0,0);
                m_Rigidbody.freezeRotation = true;
            }
        }
        if(isGameOver){
            var trans = 0f;
            var col = renderer.material.color;
            col.a = trans;
            renderer.material.color = col;
        }
        else{
            var trans = 255f;
            var col = renderer.material.color;
            col.a = trans;
            renderer.material.color = col;
        }
    }

    public static void throwTheBall(float length, float angle){
        ballThrow.m_Rigidbody.isKinematic = false;
        ballDirection(length,angle);
        m_Rigidbody.AddForce(direction  * length * m_Thrust);
        targetBallPos = direction  * length * m_Thrust;
        direction1 = direction + Vector3.right;
        m_Rigidbody.AddTorque(direction1  * length * m_Thrust);
    }
    public static void ballDirection(float length, float angle){
        float x1,y1;
        var currentPos = GameObject.FindGameObjectWithTag("Ball").transform.position;
        x1 = currentPos.x + length*Mathf.Cos((angle*Mathf.Rad2Deg*2 + 90) * Mathf.Deg2Rad);
        y1 = currentPos.y + length*Mathf.Cos(angle*2);
        direction = new Vector3(x1,y1,0) - new Vector3(currentPos.x, currentPos.y, 0);
        direction.Normalize();
    }
}