using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMouth : MonoBehaviour {
    //This script will go on HippoHead and will handle the detecting of balls and such things
    public bool eatingAllowed;
    public Text scoreText;
    public int score;
    public GameManager gm;
    public string player_name;

    // Use these to play Bite sounds
    public AudioSource AS;
    public AudioClip[] BiteSounds;
    public int NOfBiteSounds;
    System.Random R = new System.Random();

    //Effect
    public GameObject Chomp;
    public float EffectTime=0.15f;
    private bool ScalingActive;


    public bool playing;

    void playBite(int SoundNumber)
    {
        AS.clip = BiteSounds[SoundNumber];
        AS.Play();
    }

    IEnumerator ChompEffect()
    {
        Chomp.SetActive(true);
        Chomp.transform.localScale = Vector3.zero;
        ScalingActive = true;
        yield return new WaitForSeconds(EffectTime);
        ScalingActive = false;
        Chomp.SetActive(false);
        
    }

    // Use this for initialization
    void Start () {
        eatingAllowed = false;
        score = 0;
        scoreText.text = score.ToString();
        if(gm == null)
        {
            gm = FindObjectOfType<GameManager>();
        }
        playing = true;
    }

    public void allowEating()
    {
        eatingAllowed = true;
    }

    public void banEating()
    {
        eatingAllowed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            //Debug.Log("Ball all up in my face");
            if (eatingAllowed)
            {
                Eating(collision.gameObject);
            }
            else
            {
               // Debug.Log("Sorry I can't eat I'm fasting right now");
            }
        }
    }

    private void Eating(GameObject food)
    {
        playBite(R.Next(0, NOfBiteSounds));
        StartCoroutine(ChompEffect());
        food.GetComponent<BallScript>().killYourself(player_name);
        Destroy(food);
        score++;
        scoreText.text = score.ToString();

        gm.ballEaten(player_name);
    }   

    public void displayWinner(string winner)
    {
        //Debug.Log(winner + " WINS");
        playing = false;
        scoreText.text = winner + " WINS";
    }

    // Update is called once per frame
    void Update () {
		if(ScalingActive)
        {
            Chomp.transform.localScale = Vector3.Lerp(Chomp.transform.localScale, new Vector3(1,.01f, 1), 0.2f);
        }
        
	}
}
