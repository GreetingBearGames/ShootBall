using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    public static int currentScore;
    void Awake(){
        currentScore = 0;
    }

    void Update(){
        var k = (int)RandomSpawner.goList[RandomSpawner.i-1].transform.position.y;
        currentScore = k-5;
        score.text = "Score : " + currentScore;
    }
}