using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private GameObject can;
    private float time = 2.0f;
    public void Restart(){
        ballThrow.isGameOver = false;
        SceneManager.LoadScene(0);
        //StartCoroutine(ExecuteForASecond());
        //Invoke("ExecuteAfterTime", time);
    }
    IEnumerator ExecuteForASecond(){
        float timePassed = 0;
        while(timePassed < time){
            can.transform.GetChild(4).gameObject.SetActive(true);
            can.transform.GetChild(5).gameObject.SetActive(true);
            can.transform.GetChild(6).gameObject.SetActive(true);
            can.transform.GetChild(7).gameObject.SetActive(true);
            can.transform.GetChild(8).gameObject.SetActive(true);
            timePassed += Time.deltaTime;
        }
        yield return null;
    }
    public void ExecuteAfterTime(){
            can.transform.GetChild(4).gameObject.SetActive(false);
            can.transform.GetChild(5).gameObject.SetActive(false);
            can.transform.GetChild(6).gameObject.SetActive(false);
            can.transform.GetChild(7).gameObject.SetActive(false);
            can.transform.GetChild(8).gameObject.SetActive(false);
    }
}
