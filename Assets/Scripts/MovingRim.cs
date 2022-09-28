using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRim : MonoBehaviour
{
    public static bool isMovingRim = false;
    private bool dirRight = true;
    [SerializeField] private float speed;
    void Update(){
        if(RandomSpawner.timeToMoveRim){
            MoveRim();
        }
    }
    private void MoveRim(){
        if (dirRight)
            transform.Translate (Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate (-Vector2.right * speed * Time.deltaTime);
        if(transform.position.x >= 4.7f)
            dirRight = false;
        if(transform.position.x <= 0.6f)
            dirRight = true;
    }
}
