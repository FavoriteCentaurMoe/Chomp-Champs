using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
    private Rigidbody ballRB;
    public float launchStrength = 5.0f;
    public float moveSpeed = 1.0f;
    public float topSpeed = 15f;

    public GameManager gm;


    public void killYourself(string player_name)
    {
        gm.ballEaten(player_name);
    }

    void Start() {
        ballRB = gameObject.GetComponent<Rigidbody>();
        int y_Rotation = (int)Random.Range(0f, 360f);
        Quaternion start_Rotation = Quaternion.Euler(0f, y_Rotation, 0f);
        transform.rotation = start_Rotation;
        ballRB.AddForce(transform.forward * launchStrength, ForceMode.Impulse);

        if (gm == null)
        {
            gm = FindObjectOfType<GameManager>();
        }
    }

    void FixedUpdate() {
        ballRB.AddForce(ballRB.velocity.normalized * moveSpeed, ForceMode.Impulse);
        ballRB.velocity = Vector3.ClampMagnitude(ballRB.velocity, topSpeed);
    }    
}
