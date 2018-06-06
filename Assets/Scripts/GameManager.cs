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

//    private int p1Score=0;
   // private int p2Score=0;

  //  public Text p1Text;
  //  public Text p2Text;

    int count;

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

    public static int p1Score = 0;
    public static int p2Score = 0;

    public bool gameOngoing = true;


    public int ballCount = 40;

    public GameObject[] balls;

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
            Debug.Log("YOU HAVE NOT WON YET WORK HARDER");
        }
        else
        {
            Debug.Log("Oh my....you won");

            string winn;

            if(p1Score > p2Score)
            {
                winn = "Player 1 Wins!";
            }
            else if (p2Score > p1Score)
            {
                winn = "Player 2 Wins!";
            }
            else
            {
                winn = "Somehow, a tie?";
            }

            for(int i = 0; i < winTexts.Length; i++)
            {
                winTexts[i].GetComponent<Text>().text = winn;
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


        //p1Score = 0;
        //p2Score = 0;

        balls = GameObject.FindGameObjectsWithTag("Ball");
        ballCount = balls.Length;   //This is how we know how many balls are in the scene when the game starts 
        count = 0;

        Debug.Log("Hey kid, there are " + ballCount + " balls just flying around");

        isThisWinScreen();

        //// [DEPRECATED CODE]
        //if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1) {
        //    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //    p1 = players[0].GetComponent<PlayerController>();
        //    p2 = players[1].GetComponent<PlayerController>();
        //}
        AS.GetComponent<AudioSource>();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        Screen.orientation = ScreenOrientation.Portrait;

        //checkScore();
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

    public void ballEaten(string playerName)
    {
        Debug.Log("Hey look here a ball was EATEN" +playerName);
        count++;
        if(playerName == "p1")
        {
            p1Score++;
        }
        else //if(playerName == "p2")
        {
            p2Score++;   
        }
    }

    public void checkScore()
    {
      //  Debug.Log("If I don't check the score, who will?");
      //  Debug.Log("The scores are " + p1Mouth.score + "    and   " + p2Mouth.score);

     //   int totalScore = p1Mouth.score + p2Mouth.score;

    //    Debug.Log("The total score is " + totalScore+" while ballcount is "+ballCount);

       // balls = GameObject.FindGameObjectsWithTag("Ball");
      //  ballCount = balls.Length;
      //  p1WinnerText.SetActive(true);
     //   p2WinnerText.SetActive(true);



    }

    // Update is called once per frame
    void Update() {

        if(count >= 25)
        {
            Debug.Log("Just a check");
            if(gameOngoing == true)
            {
                //p1WinnerText.SetActive(true);
                //p2WinnerText.SetActive(true);

                //int p1Score = p1Mouth.score;  //for some reason accessing this value FREEZES MY PHONE =( 
                //int p2Score = p2Mouth.score;
                
                //if(p1Score > p2Score)
               // {
               //     winner = "Player 1";
               // }
               // else
              //  {
              //      winner = "Player 2";
              //  }
                Debug.Log("G A M E  O V E R ");
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
