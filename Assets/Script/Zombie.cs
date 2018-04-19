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
        if (other.gameObject.tag == "Machete")
        {
            vie -= 1;
           }
    }

    IEnumerator Burn()
    {
        yield return new WaitForSeconds(1.5f);
        vie -= 1;
    }
}
