using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {
    public bool day = true;
    public bool night = false;
    bool startday = true;
    bool startnight = false;
    public GameObject player;
    public int daynumb =0;
    public Text daytxt;
    public int killnum;
    public Text killtxt;
    public GameObject cycle;
    float timer;
    public GameObject ArrowDay;
    public GameObject ArrowNight;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        daytxt.text = "Day " + daynumb;
        cycle.transform.rotation = Quaternion.Euler(60, 0, 90 + (timer * 180 / -60));

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
            startday = false;
			player.GetComponent<PlayerDod>().life = 3;
            StartCoroutine("Cycle");
            startnight = true;
            player.GetComponent<PlayerDod>().doorout = false;
            player.GetComponent<PlayerDod>().doorin = true;
            ArrowDay.SetActive(true);
            ArrowNight.SetActive(false);
        }
    }

    void Nightfunction()
    {
        if(startnight == true)
        {
            Debug.Log("nuit");
            startnight = false;
            StartCoroutine("Cycle");
            startday = true;
            player.GetComponent<PlayerDod>().doorout = true;
			player.GetComponent<PlayerDod>().doorin = false;
            player.GetComponent<PlayerDod>().doorin = false;
            ArrowNight.SetActive(true);
            ArrowDay.SetActive(false);
        }
        
        
    }

}
