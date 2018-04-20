using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public GameObject zombie;
	private Game_Manager GC;

	void Start()
	{
		GC = GameObject.Find ("GameController").GetComponent<Game_Manager> ();
	}

	public void Spawn()
	{
		for (int i = 0; i < GC.daynumb; i++) {
			Instantiate (zombie, this.transform.position, Quaternion.identity);
		}
	}
}
