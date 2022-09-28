using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    void Start()
    {
        Camera.main.orthographicSize = 6.28f * Screen.height / Screen.width * 0.5f;
    }
}
