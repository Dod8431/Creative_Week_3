using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets;
using UnityEngine.PostProcessing;

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
	public Color night_Color;
	public GameObject GO;

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

		if(player.GetComponent<PlayerDod>().life ==0){
			GO.SetActive (true);
		}
    }

    IEnumerator Cycle()
    {
        yield return new WaitForSeconds(60);
        day =! day;
        night =! night;
    }

	void NightLight()
	{
		for (float r = 0; r < 0.08f; r += 0.01f) {
			for (float g = 0; g < 0.1f; g += 0.01f) {
				for (float b = 0; b < 0.27f; b += 0.01f) {
					for (float a = 0; a < 1f; a += 0.01f) {
						RenderSettings.ambientLight = new Color (r, g, b, a);
						GameObject.Find ("Main_Camera").GetComponent<PostProcessingBehaviour> ().enabled = true;
					}
				}
			}
		}
	}

	void DayLight()
	{
			RenderSettings.ambientLight = new Color (1f, 1f,1f, 1f);
			GameObject.Find ("Main_Camera").GetComponent<PostProcessingBehaviour> ().enabled = false;

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
			player.transform.position = GameObject.Find ("TP_Day").transform.position;
            ArrowDay.SetActive(true);
            ArrowNight.SetActive(false);
			DayLight ();
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
			player.transform.position = GameObject.Find ("TP_Night").transform.position;
            ArrowNight.SetActive(true);
            ArrowDay.SetActive(false);
			NightLight ();
			StartCoroutine (WaitAndSpawn ());
        }
        
        
    }
		
	IEnumerator WaitAndSpawn()
	{
		yield return new WaitForSeconds (3f);
		GameObject.Find ("SpawnSouthEast").GetComponent<WaveSpawner> ().Spawn ();
		GameObject.Find ("SpawnSouthWest").GetComponent<WaveSpawner> ().Spawn ();
		GameObject.Find ("SpawnNorth").GetComponent<WaveSpawner> ().Spawn ();
		yield return new WaitForSeconds (10f);
		GameObject.Find ("SpawnSouthEast").GetComponent<WaveSpawner> ().Spawn ();
		yield return new WaitForSeconds (10f);
		GameObject.Find ("SpawnNorth").GetComponent<WaveSpawner> ().Spawn ();
		yield return new WaitForSeconds (10f);
		GameObject.Find ("SpawnSouthWest").GetComponent<WaveSpawner> ().Spawn ();
	}
}
