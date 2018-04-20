using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	public GameObject GC;
	public GameObject GOText;
	// Use this for initialization
	void Start () {
		GOText.GetComponent<Text>().text = "You Survived " + GC.GetComponent<Game_Manager>().daynumb + " Days \n You Kill " + GC.GetComponent<Game_Manager>().killnum + " Zombies";
		Time.timeScale = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = 0f;

    }
}
