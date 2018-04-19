using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    SpriteRenderer zombiesp;
    public GameObject player;
    public int vie = 3;
    public GameObject GC;
    // Use this for initialization
    void Start () {
        zombiesp = this.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        GC = GameObject.Find("GameController");
    }
	
	// Update is called once per frame
	void Update () {
		/*if (this.transform.position.x < player.transform.position.x)
        {
            zombiesp.flipX = true;
        }
        else
        {
            zombiesp.flipX = false;
        }*/

        if (vie <= 0)
        {
            GC.GetComponent<Game_Manager>().killnum += 1;
            Destroy(this.gameObject);
        }
        
        if (GC.GetComponent<Game_Manager>().day == true)
        {
            StartCoroutine(Burn());
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            vie -= 1;
            Destroy(other.gameObject);
        }
    }

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") {
			StartCoroutine (Hurt ());
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			StopCoroutine (Hurt ());
		}
			
	}

    IEnumerator Burn()
    {
        yield return new WaitForSeconds(1.5f);
        vie -= 1;
    }

	IEnumerator Hurt()
	{
		yield return new WaitForSeconds (0.5f);
		GameObject.Find ("Model").GetComponent<Animator> ().Play ("Player_Hurt");
	}
}
