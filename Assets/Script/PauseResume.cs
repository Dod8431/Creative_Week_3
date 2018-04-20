using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseResume : MonoBehaviour {
    public GameObject truc;
    public GameObject PauseUI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Resume()
    {

        Time.timeScale = 1.0f;
        PauseUI.SetActive(false);
        truc.GetComponent<Pause>().isPaused = false;
    }
}
