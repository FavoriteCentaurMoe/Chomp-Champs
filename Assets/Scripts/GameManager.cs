using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // We are only planned to have 2 players for the demo, so this should do.
    private PlayerController p1;
    private PlayerController p2;

    public AudioClip[] TapSounds;
    public int NOfSounds;
    public AudioSource AS;
    System.Random R = new System.Random();

    void playTap(int SoundNumber)
    {
        AS.clip = TapSounds[SoundNumber];
        AS.Play();
    }

    void Start() {
        // If we are starting in the test scene, get the PlayerControllers.
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1) {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            p1 = players[0].GetComponent<PlayerController>();
            p2 = players[1].GetComponent<PlayerController>();
        }
        AS.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        // Process Touch Inputs.
        for (int i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                playTap(R.Next(0, NOfSounds));
              
            if (Physics.Raycast(ray, out hit)) {
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
