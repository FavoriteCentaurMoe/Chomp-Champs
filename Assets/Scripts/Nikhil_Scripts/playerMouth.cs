using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMouth : MonoBehaviour {

    //This script will go on HippoHead and will handle the detecting of balls and such things

    public bool eatingAllowed;

    public Text scoreText;

    public int score;

	// Use this for initialization
	void Start () {
        eatingAllowed = false;
        score = 0;
        scoreText.text = score.ToString();
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
            Debug.Log("Ball all up in my face");

            if (eatingAllowed)
            {
                Eating(collision.gameObject);
            }
            else
            {
                Debug.Log("Sorry I can't eat I'm fasting right now");
            }
        }
    }

    private void Eating(GameObject food)
    {
        Debug.Log("Yum, a snack has been had");
        Destroy(food);
        score++;
        scoreText.text = score.ToString();
    }


    // Update is called once per frame
    void Update () {
		
	}
}
