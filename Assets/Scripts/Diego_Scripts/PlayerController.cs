using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int playerNum;

    [SerializeField]
    private bool runChompAnim;
    [SerializeField]
    private int chompAnimState = 0; // 0 = not playing, 1 = Head rotating up. 2 = Head rotating down, 3 = head rotating to 0
    private bool nextXRotation; // If chompAnimState = 1, this = -20. If cAS == 2, this = 10. If cAS == 0, this = 0.

    void Update() {
        if (runChompAnim) {
            StartCoroutine(chompAnimation());
        }
    }

    public void processTouch(Vector3 touchPoint) {
        if (!runChompAnim)
        {
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
            runChompAnim = true;
        }
    }

    IEnumerator chompAnimation() {
        if (chompAnimState == 0 && transform.rotation.eulerAngles.x > -20)
        {
            //Debug.Log("Rotating Up.");
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-21f, transform.rotation.eulerAngles.y, 0f), .5f);
            if (transform.rotation.eulerAngles.x <= -20 + 360)
            {
                chompAnimState = 1;
            }
        }

        if (chompAnimState == 1 && (transform.rotation.eulerAngles.x > 339.5f || transform.rotation.eulerAngles.x < 10.5f))
        {
            //Debug.Log("Rotating Down.");
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(10, transform.rotation.eulerAngles.y, 0f), .7f);
            if (transform.rotation.eulerAngles.x > 9.5f && transform.rotation.eulerAngles.x < 10.5f)
            {
                chompAnimState = 2;
            }
        }
        if (chompAnimState == 2 && transform.rotation.eulerAngles.x != 0)
        {
            //Debug.Log("Rotating Back Up.");
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f), .9f);
            if (transform.rotation.eulerAngles.x < 0.05f)
            {
                chompAnimState = 0;
                runChompAnim = false;
            }
        }
        yield return null;
    }
}
