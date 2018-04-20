using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOntIme : MonoBehaviour {

    public float TimeDestroy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        TimeDestroy -= Time.deltaTime;
        if ( TimeDestroy < 0)
        {
            Destroy(gameObject);
        }
	}
}
