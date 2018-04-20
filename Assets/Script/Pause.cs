using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    bool isPaused = false;
    public GameObject PauseUI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Start"))
            {
            isPaused =! isPaused;
            }
        if(isPaused == true)
        {
            Time.timeScale = 0f;
            PauseUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            PauseUI.SetActive(false);
        }
        }

    public void Resume()
    {

        Time.timeScale = 1.0f;
        PauseUI.SetActive(false);
        isPaused = false;
    }
}
