using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDod : MonoBehaviour {
	public float speed;
	public float bulletspeed;
	GameObject Weapon;
	Quaternion lastAngle;
	public GameObject Bullet;
	float angle;
	bool canshoot = true;
	int plank = 0;
	public int life=3;
	GameObject player;
	GameObject gun;
	SpriteRenderer gunsp;
	SpriteRenderer playersp;
	public Sprite machete;
	public Sprite pistol;
	public Sprite shotgun;
	public Sprite minigun;
	float timer = 2;
	bool facingRight;
	public bool construct = false;
	bool activate = false;
	public bool doorin;
	public bool doorout;
	private Animator modelAnim;
	private int timeIdle;
	public int idleMinTime;
    public GameObject textmun;
    public GameObject munpick;
    Animator Mun;
    public GameObject pickMunSp;
    public GameObject planksp;
    public GameObject ProgresseBar;
    public GameObject ProgresseBarSliced;
    public GameObject plankreserve;
    public GameObject meacheteHit;
    ///////// Weapons /////////

    bool CACSelect = true;
	bool Pistolselect = false;
	bool ARSelect = false;
	bool MinigunSelect = false;

	public int ammoPistol =10;
	public int ammoAR =100;
	public int ammoMG =200;
	public GameObject bulletRB;

    ///////// Audio/////
    private AudioSource SourceAudio;
    public AudioClip Sound_Pistol;
    public AudioClip Sound_Shotgun;
    public AudioClip Sound_MiniGun;
    public AudioClip Sound_Cac;

    // Use this for initialization
    void Start () {
        Mun = munpick.transform.parent.GetComponent<Animator>();
        modelAnim = this.GetComponentInChildren<Animator>();
        Weapon = GameObject.Find("Gun");
		Weapon.transform.rotation = Quaternion.Euler(90, 0f, 90);
		player = GameObject.Find("Model");
		playersp = player.GetComponent<SpriteRenderer>();
		gun = GameObject.Find("gunModel");
		gunsp = gun.GetComponent<SpriteRenderer>();
		lastAngle = Quaternion.Euler(90, 180, 180);
		lastAngle = Quaternion.Euler(90, 180, 180);
		timeIdle = 0;
        SourceAudio = GetComponent<AudioSource>();
    }
		
	void Update()
	{
        plankreserve.GetComponent<Text>().text = "" + plank;
        // ANIM //
        modelAnim.SetFloat ("Run", Mathf.Abs(Input.GetAxis ("Horizontal")));
		modelAnim.SetFloat ("Run2", Input.GetAxis ("Vertical"));
		modelAnim.SetFloat ("Aim", Input.GetAxis ("Vertical1"));
		if (Input.GetAxis ("Horizontal") > 0.01f) {
			playersp.flipX = true;
		}

		if (Input.GetAxis ("Horizontal") < -0.01f) {
			playersp.flipX = false;
		}


		//

		if (activate == true)
		{
			timer -= Time.deltaTime;
            ProgresseBarSliced.GetComponent<SpriteRenderer>().size = new Vector2(-timer*30/2+30,30);
			Debug.Log(timer);
		}

		///////////////// Move /////////////////////

		if (activate == false)
		{
			float x = Input.GetAxis("Horizontal");
			if (x > 0.01) {
				x = x + speed;
			}
			float z = Input.GetAxis("Vertical");
			if (z > 0.01) {
				z = z + speed;
			}
			transform.Translate(new Vector3(x, 0, z) * speed);
		}

		if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f)
		{
			timeIdle = 0;
			modelAnim.SetBool ("Running", true);
		} else {
			timeIdle++;
			if (timeIdle > idleMinTime) {
				modelAnim.SetBool ("Running", false);
			}
		}
			
		///////////////// Aim /////////////////////

		float xaim = Input.GetAxis("Horizontal1");
		float yaim = Input.GetAxis("Vertical1");
		if (xaim != 0 && yaim != 0)
		{
			angle = Mathf.Atan2(xaim, yaim) * Mathf.Rad2Deg;
			gun.transform.rotation = Quaternion.Euler(90, 180, angle +90);
			lastAngle = gun.transform.rotation;
		}
		else
		{
			gun.transform.rotation = lastAngle;
		}

		if (yaim > 0)
		{
			gunsp.sortingOrder = 5;
		}
		else
		{
			gunsp.sortingOrder = 15;
		}

		if (xaim > 0 && !facingRight)
		{
			facingRight = !facingRight;
			playersp.flipX = false;
			Flip(gun);
		}
		else
		{
			if (xaim < 0 && facingRight)
			{
				facingRight = !facingRight;
				playersp.flipX = true;
				Flip(gun);
			}
		}

		///////////////// Shoot /////////////////////

		if (Input.GetAxis("FireTrigger") > 0)
		{
			if (CACSelect)
			{
				ShootCAC();
			}
			if (Pistolselect)
			{
				ShootPistol();
			}
			if (ARSelect)
			{
				ShootAR();
			}
			if (MinigunSelect)
			{
				ShootMiniGun();
			}
		}

		///////////////// ShootSelection /////////////////////

		if (Input.GetAxis("VerticalPad") > 0)
		{
			gunsp.sprite = machete;
			Debug.Log("cac");
			CACSelect = true;
			ARSelect = false;
			Pistolselect = false;
			MinigunSelect = false;
            textmun.GetComponent<TextMesh>().text = "∞";

        }
		if (Input.GetAxis("VerticalPad") < 0)
		{
			gunsp.sprite = shotgun;
			Debug.Log("ar");
			CACSelect = false;
			ARSelect = true;
			Pistolselect = false;
			MinigunSelect = false;
            textmun.GetComponent<TextMesh>().text = ""+ammoAR;
        }

		if (Input.GetAxis("HorizontalPad") > 0)
		{
			gunsp.sprite = pistol;
			Debug.Log("pistol");
			CACSelect = false;
			ARSelect = false;
			Pistolselect = true;
			MinigunSelect = false;
            textmun.GetComponent<TextMesh>().text = "" + ammoPistol;
        }

		if (Input.GetAxis("HorizontalPad") < 0)
		{
			gunsp.sprite = minigun;
			Debug.Log("mg");
			CACSelect = false;
			ARSelect = false;
			Pistolselect = false;
			MinigunSelect = true;
            textmun.GetComponent<TextMesh>().text = "" + ammoMG;
        }
	}

	void ShootCAC()
	{
		if (canshoot == true)
		{
            canshoot = false;
			GameObject clone;
			Quaternion dirBullet;
			dirBullet = Quaternion.Euler(0f, 0f, 0f);
			clone = Instantiate(meacheteHit, Weapon.transform.position, dirBullet) as GameObject;
		    StartCoroutine(DestroyBullet(clone,0.25f));
            StartCoroutine(CDShoot(1));
		}
	}

	void ShootPistol()
	{
		if (canshoot == true && ammoPistol>0)
		{
            SourceAudio.PlayOneShot(Sound_Pistol);
            gun.GetComponentInChildren<Animator> ().Play ("Fireshot");
			ammoPistol -= 1;
            textmun.GetComponent<TextMesh>().text = "" + ammoPistol;
            canshoot = false;
			GameObject clone;
			Quaternion dirBullet;
			dirBullet = Quaternion.Euler(60, 0f, angle + 90);
			clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
			clone.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed) + Weapon.transform.TransformDirection(Vector3.right * Random.Range(-5.0f, 5.0f));
			StartCoroutine(DestroyBullet(clone,1f));
			StartCoroutine(CDShoot(0.5f));
		}        
	}

	void ShootAR()
	{
		if (canshoot == true && ammoAR >0)
		{
            SourceAudio.PlayOneShot(Sound_Shotgun);
            gun.GetComponentInChildren<Animator> ().Play ("Fireshot");
			ammoAR -= 5;
			canshoot = false;
			GameObject clone;
			Quaternion dirBullet;
			dirBullet = Quaternion.Euler(60, 0f, angle+90);
			clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
			clone.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed)+ Weapon.transform.TransformDirection(Vector3.forward * Random.Range(-5.0f, 5.0f));
			///
			GameObject clone1;
			Quaternion dirBullet1;
			dirBullet1 = Quaternion.Euler(60, 0f, angle + 90);
			clone1 = Instantiate(Bullet, Weapon.transform.position, dirBullet1) as GameObject;
			clone1.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed)+ Weapon.transform.TransformDirection(Vector3.forward * Random.Range(5.0f, 18.0f));
			///
			GameObject clone2;
			Quaternion dirBullet2;
			dirBullet2 = Quaternion.Euler(60, 0f, angle + 90);
			clone2 = Instantiate(Bullet, Weapon.transform.position, dirBullet2) as GameObject;
			clone2.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed) + Weapon.transform.TransformDirection(Vector3.back* Random.Range(5.0f, 18.0f));
			///
			GameObject clone3;
			Quaternion dirBullet3;
			dirBullet3 = Quaternion.Euler(60, 0f, angle + 90);
			clone3 = Instantiate(Bullet, Weapon.transform.position, dirBullet3) as GameObject;
			clone3.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed) + Weapon.transform.TransformDirection(Vector3.back * Random.Range(22.0f, 30.0f));
			///
			GameObject clone4;
			Quaternion dirBullet4;
			dirBullet4 = Quaternion.Euler(60, 0f, angle + 90);
			clone4 = Instantiate(Bullet, Weapon.transform.position, dirBullet4) as GameObject;
			clone4.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed) + Weapon.transform.TransformDirection(Vector3.forward * Random.Range(22.0f, 30.0f));
            //
            //transform.Translate(-Weapon.transform.TransformDirection(Vector3.right * 0.2f));
            textmun.GetComponent<TextMesh>().text = "" + ammoAR;
            StartCoroutine(DestroyBullet(clone,1f));
			StartCoroutine(DestroyBullet(clone1,1f));
			StartCoroutine(DestroyBullet(clone2,1f));
			StartCoroutine(DestroyBullet(clone3,1f));
			StartCoroutine(DestroyBullet(clone4,1f));
			StartCoroutine(CDShoot(1));
		}
	}

	void ShootMiniGun()
	{
		if (canshoot == true && ammoMG >0)
		{
            SourceAudio.PlayOneShot(Sound_MiniGun);
            ammoMG -= 1;
			canshoot = false;
			GameObject clone;
            textmun.GetComponent<TextMesh>().text = "" + ammoMG;
            GameObject cloneDouille;
			Quaternion dirBullet;
			dirBullet = Quaternion.Euler(60, 0f, angle+90);
			clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
			gun.GetComponentInChildren<Animator> ().Play ("Fireshot");
			clone.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed) + Weapon.transform.TransformDirection(Vector3.right * Random.Range(-5.0f, 5.0f));
			cloneDouille = Instantiate (bulletRB, Weapon.transform.position, dirBullet) as GameObject;
			cloneDouille.GetComponent<Rigidbody> ().AddForce (new Vector3 (Random.Range (250f, 500f), Random.Range (250f, 500f), Random.Range (250f, 500f)), ForceMode.Impulse);
			//transform.Translate(-Weapon.transform.TransformDirection(Vector3.up * 1));
			StartCoroutine(DestroyBullet(clone,1f));
			StartCoroutine (DestroyBullet (cloneDouille, 10f));
			StartCoroutine(CDShoot(0.1f));
		}
	}

	IEnumerator DestroyBullet (GameObject bullet, float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(bullet);
	}

	IEnumerator CDShoot (float wait)
	{
		yield return new WaitForSeconds(wait);
		canshoot = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Zombie" || other.gameObject.tag == "ZombiePlayer")
		{
			life -= 1;
		}

		if (other.gameObject.tag =="Plank")
		{
			plank += 3;
            pickMunSp.SetActive(false);
            planksp.SetActive(true);
            munpick.GetComponent<TextMesh>().text = "+3 Plank";
            Mun.Play("PickUpMun");
            Destroy(other.gameObject);
		}

		if (other.gameObject.tag == "AmmoPistol")
		{
			ammoPistol += 10;
            pickMunSp.SetActive(true);
            planksp.SetActive(false);
            munpick.GetComponent<TextMesh>().text = "+15 Pistol";
            Mun.Play("PickUpMun");
            Destroy(other.gameObject);
		}

		if (other.gameObject.tag == "AmmoSG")
		{
            pickMunSp.SetActive(true);
            planksp.SetActive(false);
            ammoAR += 25;
            munpick.GetComponent<TextMesh>().text = "+5 ShotGun";
            Mun.Play("PickUpMun");
            Destroy(other.gameObject);
		}

		if (other.gameObject.tag == "AmmoMG")
		{
            pickMunSp.SetActive(true);
            planksp.SetActive(false);
            ammoMG += 35;
            munpick.GetComponent<TextMesh>().text = "+25 MiniGun";
            Mun.Play ("PickUpMun");
            Destroy(other.gameObject);
		}

		if (other.gameObject.tag == "Doorin"&& doorin)
		{
			transform.position = new Vector3(0, 0, 0);
		}
		if (other.gameObject.tag == "Doorout" && doorout)
		{
			transform.position = new Vector3(0, 0, 0);
		}
	}

	void OnTriggerStay (Collider other)
	{

		if (other.gameObject.tag == "Barrier" && construct == false && plank>2 && other.gameObject.GetComponent<Barrier>().life < 30)
		{
			
			if (Input.GetButtonDown("Fire1"))
			{
				activate = true;
                ProgresseBar.SetActive(true);
			}

			if (Input.GetButtonUp("Fire1")&&construct==false)
			{
				activate = false;
				timer = 2;
				Debug.Log("cancel");
                ProgresseBar.SetActive(false);
            }
			if (timer <= 0)
			{
				activate = false;
                ProgresseBar.SetActive(false);
                Debug.Log("bravo");
				construct = true;
				plank -= 3;
                pickMunSp.SetActive(false);
                planksp.SetActive(true);
                munpick.GetComponent<TextMesh>().text = "-3 Plank";
                Mun.Play("PickUpMun");
                timer = 2;
				other.gameObject.GetComponent<Barrier> ().life = 30;
				construct = false;
            }
		}

	}

	void Flip(GameObject swap)
	{
		Vector3 theScale = swap.transform.localScale;
		theScale.y *= -1;
		swap.transform.localScale = theScale;
	} 
}
