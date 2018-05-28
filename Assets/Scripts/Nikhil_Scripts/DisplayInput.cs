using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInput : MonoBehaviour {

    //I made this script to see the inputs from touching and stuff 

    //Touches can be tracked with fingerID

    //tap Count?

    public Text touchCount;
    public Text touchPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        touchCount.text = "Touch Count : " + Input.touchCount;
        
        if(Input.touchCount > 0)
        {
            Touch myTouch = Input.GetTouch(0);
            touchPosition.text = "Touch 1 Positon : " +myTouch.position;
        }



    }
}
