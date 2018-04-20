using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour {

	private string current_Tag;

	void Start () 
	{
		current_Tag = this.tag; 
	}

	public void Respawn()
	{
		StartCoroutine (WaitRespawn ());	
	}
		
	IEnumerator WaitRespawn()
	{
		this.tag = "Untagged";
		this.GetComponentInChildren<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (60f);
		this.tag = current_Tag;
		this.GetComponentInChildren<SpriteRenderer> ().enabled = true;
	}
}
