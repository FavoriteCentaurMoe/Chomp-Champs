using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // We are only planned to have 2 players for the demo, so this should do.
    private PlayerController p1;
    private PlayerController p2;

    void Start() {
        // If we are starting in the test scene, get the PlayerControllers.
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1) {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            p1 = players[0].GetComponent<PlayerController>();
            p2 = players[1].GetComponent<PlayerController>();
        }
    }

	// Update is called once per frame
	void Update () {
        // Process Touch Inputs.
        for (int i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) {
                    //Debug.Log("You hit a thing! Here's what it was: " + hit.transform.name);
                    //Debug.Log("Here is where you hit the thing: " + hit.point);

                    Vector3 lookPoint = new Vector3(hit.point.x, 0, hit.point.z);
                    if (hit.point.z < 0) {
                        p1.processTouch(lookPoint);
                    }
                    else if (hit.point.z > 0) {
                        p2.processTouch(lookPoint);
                    }
                }                    
            }
        }
	}
}
