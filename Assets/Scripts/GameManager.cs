using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    // We are only planned to have 2 players for the demo, so this should do.
    [SerializeField]
    private PlayerController p1;
    [SerializeField]
    private PlayerController p2;

    //Use these to play tapping sounds
    public AudioClip[] TapSounds;
    public int NOfTapSounds;
    System.Random R = new System.Random();
    public AudioSource AS;
    private bool canPlaySounds;
    
    void playTap(int SoundNumber)
    {
        if (canPlaySounds)
        {
            AS.clip = TapSounds[SoundNumber];
            AS.Play();
        }
    }
    

    void Start() {
        // If we run into other GameManagers, DESTORY THEM!
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        //// [DEPRECATED CODE]
        //if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1) {
        //    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //    p1 = players[0].GetComponent<PlayerController>();
        //    p2 = players[1].GetComponent<PlayerController>();
        //}
        AS.GetComponent<AudioSource>();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode) {
        if (instance == this) {
            if (scene.buildIndex == 0)
            {
                canPlaySounds = false;
            }
            else if (scene.buildIndex == 1)
            {
                canPlaySounds = true;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                p1 = players[0].GetComponent<PlayerController>();
                p2 = players[1].GetComponent<PlayerController>();
            }
        }
    }

    // Update is called once per frame
    void Update() {
        // Process Touch Inputs.
        for (int i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                playTap(R.Next(0, NOfTapSounds));
              
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
