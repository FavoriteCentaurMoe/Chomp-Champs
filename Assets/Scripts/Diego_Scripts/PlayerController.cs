using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public string playerName;

    public void processTouch(Vector3 touchPoint) {
        transform.LookAt(touchPoint);    
    }
}
