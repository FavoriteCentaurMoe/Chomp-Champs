using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    private static GameManager instance;
    // We are only planned to have 2 players for the demo, so this should do.
    [SerializeField]
    private PlayerController p1;
    [SerializeField]
    private PlayerController p2;
    public playerMouth p1Mouth; //needed for the end of game
    public playerMouth p2Mouth;

    //Use these to play tapping sounds
    public AudioClip[] TapSounds;
    public int NOfTapSounds;
    System.Random R = new System.Random();
    public AudioSource AS;
    private bool canPlaySounds;

    string winner = "TIE";
    public GameObject p1WinnerText;
    public GameObject p2WinnerText;

    public int p1Score = 0;
    public int p2Score = 0;

    public GameObject[] balls;
    public bool gameOngoing = true;
    public int count;
    public int ballCount = 40;

    private GameObject background;
    public Material p1Material;
    public Material p2Material;

    public Text testText;

    void playTap(int SoundNumber)
    {
        if (canPlaySounds)
        {
            AS.clip = TapSounds[SoundNumber];
            AS.Play();
        }
    }

    public IEnumerator endIt()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

    public IEnumerator winScreen()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }


    public void isThisWinScreen()
    {
        GameObject[] winTexts = GameObject.FindGameObjectsWithTag("winText");

        if(winTexts.Length == 0)
        {
            //Debug.Log("YOU HAVE NOT WON YET WORK HARDER");
        }
        else
        {
            //Debug.Log("Oh my....you won");
            if(p1Score > p2Score)
            {
                background.GetComponent<Renderer>().material = p1Material;
            }
            else if (p2Score > p1Score)
            {
                background.GetComponent<Renderer>().material = p2Material;
            }

            for (int i = 0; i < winTexts.Length; i++)
            {
                if (p1Score > p2Score)
                    winTexts[i].GetComponent<Text>().text = "Player 1 Wins!";
                else if (p2Score > p1Score)
                    winTexts[i].GetComponent<Text>().text = "Player 2 Wins!";
            }
            StartCoroutine(endIt());
        }
    }


    void Start() {
        // If we run into other GameManagers, DESTORY THEM!
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        isThisWinScreen();

        AS.GetComponent<AudioSource>();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode) {
        if (instance == this) {
            if (scene.buildIndex == 0)
            {
                canPlaySounds = false;
                count = 0;

                p1Score = 0;
                p2Score = 0;
            }
            else if (scene.buildIndex == 1)
            {
                canPlaySounds = true;
                gameOngoing = true;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                p1 = players[0].GetComponent<PlayerController>();
                p2 = players[1].GetComponent<PlayerController>();

                GameObject[] playerheads = GameObject.FindGameObjectsWithTag("playerHead"); // This has the orders backwards for some reason...
                p1Mouth = playerheads[1].GetComponent<playerMouth>();
                p2Mouth = playerheads[0].GetComponent<playerMouth>();
                p1Mouth.gm = instance;
                p2Mouth.gm = instance;

                balls = GameObject.FindGameObjectsWithTag("Ball");
                ballCount = balls.Length;   //This is how we know how many balls are in the scene when the game starts 
                count = 0;

                p1Score = 0;
                p2Score = 0;

                //DELETE
                testText = GameObject.FindGameObjectWithTag("test").GetComponent<Text>();

            }
            else if (scene.buildIndex == 2)
            {
                background = GameObject.FindGameObjectWithTag("Background");
                isThisWinScreen();
            }
        }
    }

    public void ballEaten(string playerName)
    {
        if(playerName == "p1")
        {
            p1Score++;
        }
        else if (playerName == "p2")
        {
            p2Score++;   
        }
        count++;
    }

    // Update is called once per frame
    void Update() {
        if (testText != null) {
            testText.text = count.ToString();
        }
        if(count >= 25)
        {
            //Debug.Log("Just a check");
            if(gameOngoing == true)
            {
                //Debug.Log("G A M E  O V E R ");
                StartCoroutine(winScreen());
                gameOngoing = false;
            }

        }
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
