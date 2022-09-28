using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PotaController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    EventSystem m_EventSystem;
    public float scaleSpeed = 100f;
    private float _startingPosition;
    private float deltaTouch;
    private Vector2 startPosition;
    private float scalingFactor = 1f;
    private Vector3 startPosZ;
    public static Vector3 startPos;
    private float length;
    [SerializeField]
    private float duration = 1f;
    private Quaternion startAngle; 
    void Start(){
        startPosZ = transform.localScale;
        startPos = transform.position;
        startAngle = transform.parent.localRotation;
    }

    void Update () {
        if(Input.touchCount > 0){
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)){
                if(hit.collider != null){
                    GameObject m_MyGameObject = hit.collider.gameObject;
                    m_EventSystem.SetSelectedGameObject(m_MyGameObject);
                    Touch touch = Input.GetTouch(0);
                    switch (touch.phase){
                        case TouchPhase.Began:
                            _startingPosition = touch.position.x;
                            break;
                        case TouchPhase.Moved:
                            if (_startingPosition > touch.position.x)
                            {
                                _startingPosition = _startingPosition - touch.position.x;
                            }
                            else if (_startingPosition < touch.position.x)
                            {
                                _startingPosition =  touch.position.x - _startingPosition;
                            }
                            break;
                        case TouchPhase.Stationary:
                            _startingPosition = touch.position.x;
                            break;
                        case TouchPhase.Ended:
                            Debug.Log("Touch Phase Ended.");
                            break;
                    }
                }
            }
        }
        
    }
    void OnEnable(){
        m_EventSystem = EventSystem.current;
    }
    public void OnBeginDrag(PointerEventData eventData) {
        startPosition = Vector2.zero;
        ballThrow.m_Rigidbody.isKinematic = true;
    }

    public void OnDrag(PointerEventData eventData) {
        ballThrow.m_Rigidbody.isKinematic = true;
        var x = transform.localScale.x;
        var y = transform.localScale.y;
        startPosition += eventData.delta;
        var scaleZ = Mathf.Abs(startPosition.y);
        if(scaleZ < 200){
            scaleZ = 200;
            length = 0;
            transform.localScale = new Vector3(x, y, scaleZ);
        }
        if(scaleZ > 500){
            transform.localScale = new Vector3(x, y, 500);
            length = 3;
        }
        else{
            transform.localScale = new Vector3(x, y, scaleZ * scalingFactor);
            length = (scaleZ - 200) / 100;
        }
        transform.parent.localRotation = Quaternion.Euler(0, 0, (startPosition.x)*0.1f); 
    }
    public void OnEndDrag(PointerEventData eventData) {
        ballThrow.throwTheBall(length, transform.parent.rotation.z);
        StartCoroutine(LerpPositionAndRotation(startPosZ, duration, startAngle));

    }
    IEnumerator LerpPositionAndRotation(Vector3 targetScale, float duration, Quaternion targetRotation)
    {
        float time = 0;
        Vector3 startScale = transform.localScale;
        Quaternion startRotation = transform.parent.localRotation;
        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            transform.parent.localRotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
        transform.parent.localRotation = targetRotation;
    }
}
