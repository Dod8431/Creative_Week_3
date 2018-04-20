using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class Zombie : MonoBehaviour {
    SpriteRenderer zombiesp;
    public GameObject player;
    public int vie = 3;
    public GameObject GC;
	private bool check;
	private bool check_damage_Barrier;
	public GameObject firstBarrierHit;
    // Use this for initialization
    ///////// Audio/////
    private AudioSource SourceAudio;
    public AudioClip Sound_Scream;
    public AudioClip Sound_Dead;
    public GameObject DeadSound;
    void Start () {
        zombiesp = this.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        GC = GameObject.Find("GameController");
        SourceAudio = GetComponent<AudioSource>();
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
            Instantiate(DeadSound, transform.position, transform.rotation);
            SourceAudio.PlayOneShot(Sound_Dead);
            GC.GetComponent<Game_Manager>().killnum += 1;
            Destroy(this.gameObject);
        }
        
        if (GC.GetComponent<Game_Manager>().day == true)
        {
            StartCoroutine(Burn());
        }


		this.GetComponentInChildren<Animator> ().SetFloat ("Run", Mathf.Abs(this.GetComponent<NavMeshAgent>().velocity.x));
		this.GetComponentInChildren<Animator> ().SetFloat ("Run", Mathf.Abs(this.GetComponent<NavMeshAgent>().velocity.z));

		if (firstBarrierHit.GetComponent<Barrier> ().life <= 0) {
			this.GetComponent<BehaviorTree> ().SetVariableValue ("barrierDestroy", true);
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

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") {
			if (check == false) {
				StartCoroutine (Hurt ());
			}
		}

		if (other.gameObject.tag == "Barrier") {
			firstBarrierHit = other.gameObject;
			if (check_damage_Barrier == false) {
				StartCoroutine (BarrierHurt (other.gameObject));
			}
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
		check = true;
        SourceAudio.PlayOneShot(Sound_Scream);
		this.GetComponentInChildren<Animator> ().SetTrigger ("Attack");
		GameObject.Find ("FX_Blood_Human").GetComponent<ParticleSystem> ().Play ();
		yield return new WaitForSeconds (0.25f);
		GameObject.Find ("Model").GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (0.25f);
		GameObject.Find ("Model").GetComponent<SpriteRenderer> ().color = Color.white;
		GameObject.Find ("Player").GetComponent<PlayerDod> ().life -= 1;
		GameObject.Find ("FX_Blood_Human").GetComponent<ParticleSystem> ().Stop ();
		check = false;
	}

	IEnumerator BarrierHurt(GameObject barrier)
	{
		check_damage_Barrier = true;
		this.GetComponentInChildren<Animator> ().SetTrigger ("Attack");
		yield return new WaitForSeconds (2);
		barrier.GetComponent<Barrier> ().life -= 1;
		check_damage_Barrier = false;

	}
		
}
