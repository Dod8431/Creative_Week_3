using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barrier : MonoBehaviour {

	public int life = 30;
	public SpriteRenderer[] barrier_Sprites;
	public BoxCollider[] box_Colliders;
	private NavMeshObstacle obstacle_Mesh;
	public List<GameObject> zombies;

	void Start () 
	{
		barrier_Sprites = this.GetComponentsInChildren<SpriteRenderer> ();
		box_Colliders = this.GetComponentsInChildren<BoxCollider> ();
		obstacle_Mesh = this.GetComponent<NavMeshObstacle> ();
	}

	void Update () 
	{
		if (life < 15) {
			barrier_Sprites [2].enabled = false;
			barrier_Sprites [3].enabled = false;
		}

		if (life <= 0) {
			barrier_Sprites [0].enabled = false;
			barrier_Sprites [1].enabled = false;
			box_Colliders [0].enabled = false;
			//box_Colliders [1].enabled = false;
			obstacle_Mesh.enabled = false;
		}

		if (life == 30)
		{
			barrier_Sprites [2].enabled = true;
			barrier_Sprites [3].enabled = true;
			barrier_Sprites [0].enabled = true;
			barrier_Sprites [1].enabled = true;
			box_Colliders [0].enabled = true;
			obstacle_Mesh.enabled = true;
		}
	}
}
