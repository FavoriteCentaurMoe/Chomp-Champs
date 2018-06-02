using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int playerNum;

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
    }
}
