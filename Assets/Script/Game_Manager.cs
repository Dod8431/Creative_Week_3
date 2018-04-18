using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {
    public bool day = true;
    public bool night = false;
    bool startday = false;
    bool startnight = true;
    public GameObject player;
    public int daynumb;
    public Text daytxt;
    public int killnum;
    public Text killtxt;

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
        killtxt.text = ""+killnum;
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
            daytxt.text = "Day " + daynumb;
            startday = false;

			player.GetComponent<PlayerDod>().life = 3;
            StartCoroutine("Cycle");
            startnight = true;
			player.GetComponent<PlayerDod>().doorout = false;
			player.GetComponent<PlayerDod>().doorin = true;

            player.GetComponent<PlayerDod>().life = 3;
            StartCoroutine("Cycle");
            startnight = true;
            player.GetComponent<PlayerDod>().doorout = false;
            player.GetComponent<PlayerDod>().doorin = true;

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
			player.GetComponent<PlayerDod>().doorin = false;
            player.GetComponent<PlayerDod>().doorin = false;
        }
        
        
    }

}
