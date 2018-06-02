using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int playerNum;

    //[SerializeField]
    //private bool readyForInput = true;
    //private bool runChomp = false;
    //[SerializeField]
    //private bool chompState = false;
    //[SerializeField]
    //private bool finishChomp = false;
    //[SerializeField]
    //private bool atStartRotation = false;
    //private Quaternion mouthOpen;
    //private Quaternion mouthClosed;
    //private Quaternion originalRotation;

    void Update() {
        //if (runChomp) {
        //    mouthOpen = Quaternion.Euler(-20f, transform.rotation.eulerAngles.y, 0f);
        //    mouthClosed = Quaternion.Euler(10f, transform.rotation.eulerAngles.y, 0f);
        //    originalRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0f);
        //    StartCoroutine(chompAnimation());
        //    Debug.Log("Coroutine called.");
        //}
    }

    public void processTouch(Vector3 touchPoint) {
            transform.LookAt(touchPoint);
            // Is this the best to clamp the Y-axis rotation? Prolly not. I blame Quaternions.
            if (playerNum == 1)
            {
                if (transform.rotation.eulerAngles.y < 280 && transform.rotation.eulerAngles.y > 80)
                {
                    float yRot = transform.rotation.eulerAngles.y;
                    if (yRot < 280 && yRot > 180)
                        yRot = 280f;
                    else if (yRot > 80 && yRot <= 180)
                        yRot = 80f;
                    transform.rotation = Quaternion.Euler(0f, yRot, 0f);
                }
            }
            else if (playerNum == 2)
            {
                if (transform.rotation.eulerAngles.y > 260 || transform.rotation.eulerAngles.y < 100)
                {
                    float yRot = transform.rotation.eulerAngles.y;
                    if (yRot < 100)
                        yRot = 100f;
                    else if (yRot > 260)
                        yRot = 260f;
                    transform.rotation = Quaternion.Euler(0f, yRot, 0f);
                }
            }
        
        //readyForInput = false;
        //runChomp = true;
    }

    //IEnumerator chompAnimation() {
    //    if (!chompState && !finishChomp)
    //        transform.rotation = Quaternion.Slerp(transform.rotation, mouthOpen, .8f);
    //    else if (chompState && !finishChomp)
    //        transform.rotation = Quaternion.Slerp(transform.rotation, mouthClosed, .8f);
    //    else if (finishChomp)
    //        transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, .8f);

    //    if (!chompState && transform.rotation == mouthOpen) {
    //        chompState = true;
    //        Debug.Log("Mouth Fully Opened.");
    //    }
    //    if (chompState && transform.rotation == mouthClosed) {
    //        Debug.Log("Mouth Fully Closed.");
    //        finishChomp = true;
    //    }
    //    if (finishChomp && transform.rotation == originalRotation) {
    //        Debug.Log("Back to Normal.");
    //        atStartRotation = true;
    //    }
    //    if (atStartRotation) {
    //        chompState = false;
    //        finishChomp = false;
    //        readyForInput = true;
    //        atStartRotation = false;
    //        runChomp = false;
    //        Debug.Log("Animiation Complete.");
    //    }

    //    yield return null;
    //}
}
