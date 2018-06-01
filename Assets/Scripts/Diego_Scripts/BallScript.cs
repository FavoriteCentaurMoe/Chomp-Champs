using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
    private Rigidbody ballRB;
    public float launchStrength = 5.0f;
    public float moveSpeed = 1.0f;
    public float topSpeed = 15f;

    void Start() {
        ballRB = gameObject.GetComponent<Rigidbody>();

        //This just makes the ball have a random rotation.
        int y_Rotation = (int)Random.Range(0f, 360f);
        Quaternion start_Rotation = Quaternion.Euler(0f, y_Rotation, 0f);
        transform.rotation = start_Rotation;
        ballRB.AddForce(transform.forward * launchStrength, ForceMode.Impulse);
    }

    void FixedUpdate() {
        ballRB.AddForce(ballRB.velocity.normalized * moveSpeed, ForceMode.Impulse);
        ballRB.velocity = Vector3.ClampMagnitude(ballRB.velocity, topSpeed);
    }

    // Kinda useless now. delete if needed.
    void OnCollisionEnter(Collision coll) {
        if (coll.transform.tag == "WallX")
        {
            //Debug.Log("Hit a wall on the X Axis.");
            //ContactPoint contact = coll.contacts[0];

            //Debug.DrawRay(contact.point, contact.normal, Color.white, 2f);
            //Debug.Log(Quaternion.Euler(contact.normal).eulerAngles);
            //Debug.Log(Mathf.Atan2(transform.rotation.eulerAngles.y, Quaternion.Euler(contact.normal).eulerAngles.x) * Mathf.Rad2Deg);


        }
        else if (coll.transform.tag == "WallZ")
        {
            //Debug.Log("Hit a wall on the Z Axis.");
            //ContactPoint contact = coll.contacts[0];          

            //Debug.DrawRay(contact.point, contact.normal, Color.white, 2f);
            //Debug.Log(Quaternion.Euler(contact.normal).eulerAngles);
            //Debug.Log(Mathf.Atan2(transform.rotation.eulerAngles.y, Quaternion.Euler(contact.normal).eulerAngles.z) * Mathf.Rad2Deg);

        }
    }

    // Private Functions
    
}
