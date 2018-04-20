using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Wall") {
			Debug.Log ("cc");
			Destroy (this.gameObject);
		}
	}
}
