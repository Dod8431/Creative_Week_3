using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {
    public bool day = true;
    public bool night = false;


	// Use this for initialization
	void Start () {
        StartCoroutine("Cycle");
    }
	
	// Update is called once per frame
	void Update () {
        if (day)
        {

        }else
        {

        }
		
	}

    IEnumerator Cycle()
    {
        yield return new WaitForSeconds(60);
        day =! day;
        night =! night;
    }

    void Dayfunction()
    {


        StartCoroutine ("Cycle");
    }

    void Nightfunction()
    {


        StartCoroutine("Cycle");
    }

}
