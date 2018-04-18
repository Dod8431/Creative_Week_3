using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {
    public bool day = true;
    public bool night = false;
    bool startday = false;
    bool startnight = true;
    public GameObject player;
    public int daynumb;


	// Use this for initialization
	void Start () {
        StartCoroutine("Cycle");
    }
	
	// Update is called once per frame
	void Update () {
        if (day)
        {
            Dayfunction();
        }else
        {
            Nightfunction();
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
        if (startday == true)
        {
            Debug.Log("jour");
            daynumb += 1;
            startday = false;
<<<<<<< HEAD
			player.GetComponent<PlayerDod>().life = 3;
            StartCoroutine("Cycle");
            startnight = true;
			player.GetComponent<PlayerDod>().doorout = false;
			player.GetComponent<PlayerDod>().doorin = true;
=======
            player.GetComponent<PlayerDod>().life = 3;
            StartCoroutine("Cycle");
            startnight = true;
            player.GetComponent<PlayerDod>().doorout = false;
            player.GetComponent<PlayerDod>().doorin = true;
>>>>>>> f121f90f25f680a2053b751c0bacbfae8773ebbf
        }
    }

    void Nightfunction()
    {
        if(startnight == true)
        {
            Debug.Log("nuit");
            startnight = false;
            player.transform.position = new Vector3(0, 0, 0);
            StartCoroutine("Cycle");
            startday = true;
            player.GetComponent<PlayerDod>().doorout = true;
<<<<<<< HEAD
			player.GetComponent<PlayerDod>().doorin = false;
=======
            player.GetComponent<PlayerDod>().doorin = false;
>>>>>>> f121f90f25f680a2053b751c0bacbfae8773ebbf
        }
        
        
    }

}
