using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RimController : MonoBehaviour
{
    public collisionDetect cd;
    public float scaleSpeed = 100f;
    private float _startingPositionx, _startingPositiony;
    private float deltaTouchx, deltaTouchy;
    private Vector2 startPosition;
    private Vector3 startPosZ;
    public static Vector3 startPos;
    private float length;
    [SerializeField]private float duration = 0.1f, rimMoveSpeed;
    private Quaternion startAngle;
    public static GameObject go;
    [SerializeField] private GameObject can;
    public static bool isGameStarted;
    private bool dirRight = true;
    public static bool isBallOnAir;
    void Start(){
        isGameStarted = false;
        isBallOnAir = false;
        OnBeginDragging();
        duration = 0.1f;
        length = 0;
        
    }
    void Update () {
        if(isGameStarted)
            rimMove();
        else{
            if(can.transform.GetChild(2).gameObject.activeInHierarchy){
                can.transform.GetChild(3).gameObject.SetActive(false);
            }
            ballThrow.isGameOver = false;
        }
        if(Input.touchCount > 0 && ballThrow.m_Rigidbody.velocity.x == 0){
            Touch touch = Input.GetTouch(0);
            if(isGameStarted == false && ballThrow.isGameOver == false){
                can.transform.GetChild(2).gameObject.SetActive(false);
                //can.transform.GetChild(0).gameObject.SetActive(true);
                can.transform.GetChild(1).gameObject.SetActive(true);
                isGameStarted = true;
            }
            else if(ballThrow.isGameOver == true){
                can.transform.GetChild(2).gameObject.SetActive(false);
                //can.transform.GetChild(0).gameObject.SetActive(true);
                can.transform.GetChild(1).gameObject.SetActive(true);
                isGameStarted = true;
                ballThrow.isGameOver = false;
                SceneManager.LoadScene(0);
            }
            else{
                switch (touch.phase){
                    case TouchPhase.Began:
                        OnBeginDragging();
                        break;
                    case TouchPhase.Moved:
                        OnDragging(touch.deltaPosition);
                        break;
                    case TouchPhase.Ended:
                        EndDragging();
                        break;
                }
            }
        }
    }
    public void OnBeginDragging(){
        go = collisionDetect.go;
        if(go != null){
            startPosZ = go.transform.localScale;
            startPos = go.transform.position;
            startAngle = go.transform.parent.localRotation;
            startPosition = Vector2.zero;
            var newBallPos = startPos;
            newBallPos.y -= 0.03f;
        }
    }
    public void OnDragging(Vector2 deltaPos){
        isBallOnAir = false;
        ballThrow.m_Rigidbody.isKinematic = true;
        var x = go.transform.localScale.x;
        var y = go.transform.localScale.y;
        if(deltaPos == null){
            return;
        }
        startPosition += deltaPos;
        var scaleZ = Mathf.Abs(startPosition.y);
        if(scaleZ < 200){
            scaleZ = 200;
            length = 0;
            go.transform.localScale = new Vector3(x, y, scaleZ);
        }
        if(scaleZ > 500){
            scaleZ = 500;
            go.transform.localScale = new Vector3(x, y, scaleZ);
            length = 3;
        }
        else{
            go.transform.localScale = new Vector3(x, y, scaleZ);
            length = (scaleZ - 200) / 100;
        }
        go.transform.parent.localRotation = Quaternion.Euler(0, 0, (startPosition.x)*0.15f);
    }
    public void EndDragging() {
        ballThrow.m_Rigidbody.isKinematic = false;
        go.GetComponent<BoxCollider>().enabled = false;
        if(length != 0){
            ballThrow.throwTheBall(length, go.transform.parent.localRotation.z);
            length = 0;
            isBallOnAir = true;
        }
        StartCoroutine(LerpPositionAndRotation(startPosZ, duration, startAngle));
        StartCoroutine(ExecuteAfterTime(0.5f));
    }
    IEnumerator ExecuteAfterTime(float time){
        yield return new WaitForSeconds(time);
        go.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator LerpPositionAndRotation(Vector3 targetScale, float duration, Quaternion targetRotation){
        if(isGameStarted){
            float time = 0;
            Vector3 startScale = go.transform.localScale;
            Quaternion startRotation = go.transform.parent.localRotation;
            while (time < duration){
                go.transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
                go.transform.parent.localRotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            go.transform.localScale = targetScale;
            go.transform.parent.localRotation = targetRotation;
        }
        
    }
    public void rimMove(){
        if(RandomSpawner.timeToMoveRim){
            if (dirRight)
                RandomSpawner.goList[RandomSpawner.i - 1].transform.Translate(Vector3.right * rimMoveSpeed * Time.deltaTime);
            else
                RandomSpawner.goList[RandomSpawner.i - 1].transform.Translate(-Vector3.right * rimMoveSpeed * Time.deltaTime);
            if(RandomSpawner.goList[RandomSpawner.i - 1].transform.position.x >= 4.7f)
                dirRight = false;
            if(RandomSpawner.goList[RandomSpawner.i - 1].transform.position.x <= 0.6f)
                dirRight = true;
        }
    }
}