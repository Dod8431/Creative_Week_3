using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
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
    public Sprite backplayer;
    public Sprite machete;
    public Sprite pistol;
    public Sprite shotgun;
    public Sprite minigun;

    ///////// Weapons /////////
    /*bool CAC = true;
    bool Pistol = false;
    bool AR = false;
    bool MiniGun = false;*/

    bool CACSelect = true;
    bool Pistolselect = false;
    bool ARSelect = false;
    bool MinigunSelect = false;

    public int ammoPistol =10;
    public int ammoAR =100;
    public int ammoMG =200;

    // Use this for initialization
    void Start () {
        Weapon = GameObject.Find("Gun");
        Weapon.transform.rotation = Quaternion.Euler(90, 0f, 90);
        player = GameObject.Find("Model");
        playersp = player.GetComponent<SpriteRenderer>();
        gun = GameObject.Find("gunModel");
        gunsp = gun.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {


        ///////////////// Move /////////////////////

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(x, 0, z) * speed);

        ///////////////// Aim /////////////////////

        float xaim = Input.GetAxis("Horizontal1");
        float yaim = Input.GetAxis("Vertical1");
        if (xaim != 0 && yaim != 0)
        {
            angle = Mathf.Atan2(xaim, yaim) * Mathf.Rad2Deg;
            Weapon.transform.rotation = Quaternion.Euler(90, 0f, angle );
            lastAngle = Weapon.transform.rotation;
        }
        else
        {
            Weapon.transform.rotation = lastAngle;
        }

        if (xaim > 0)
        {
            playersp.sprite = backplayer;
            gunsp.sortingOrder = 1;
        }
        else
        {
            gunsp.sortingOrder = 3;
        }

        if (xaim > 0)
        {
            playersp.flipX = true;
            gun.transform.localScale = gun.transform.localScale + Vector3.left;
        }else
        {
            playersp.flipX = false;
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
        }
        if (Input.GetAxis("VerticalPad") < 0)
            {
            gunsp.sprite = shotgun;
            Debug.Log("ar");
            CACSelect = false;
            ARSelect = true;
            Pistolselect = false;
            MinigunSelect = false;
        }

        if (Input.GetAxis("HorizontalPad") > 0)
            {
            gunsp.sprite = pistol;
            Debug.Log("pistol");
            CACSelect = false;
            ARSelect = false;
            Pistolselect = true;
            MinigunSelect = false;
        }

        if (Input.GetAxis("HorizontalPad") < 0)
            {
            gunsp.sprite = minigun;
            Debug.Log("mg");
            CACSelect = false;
            ARSelect = false;
            Pistolselect = false;
            MinigunSelect = true;
        }
    }

    void ShootCAC()
    {
        if (canshoot == true)
        {
            canshoot = false;
            GameObject clone;
            Quaternion dirBullet;
            dirBullet = Quaternion.Euler(60, angle, 0f);
            clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
            clone.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up* bulletspeed);
            StartCoroutine(DestroyBullet(clone));
            StartCoroutine(CDShoot(2));
        }
    }

    void ShootPistol()
    {
        if (canshoot == true && ammoPistol>0)
        {
            ammoPistol -= 1;
            canshoot = false;
            GameObject clone;
            Quaternion dirBullet;
            dirBullet = Quaternion.Euler(60, 0f, angle);
            clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
            clone.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed) + Weapon.transform.TransformDirection(Vector3.right * Random.Range(-5.0f, 5.0f));
            StartCoroutine(DestroyBullet(clone));
            StartCoroutine(CDShoot(1));
        }        
    }

    void ShootAR()
    {
        if (canshoot == true && ammoAR >0)
        {
            ammoAR -= 1;
            canshoot = false;
            GameObject clone;
            Quaternion dirBullet;
            dirBullet = Quaternion.Euler(60, 0f, angle);
            clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
            clone.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed)+ Weapon.transform.TransformDirection(Vector3.right * Random.Range(-5.0f, 5.0f));
            ///
            GameObject clone1;
            Quaternion dirBullet1;
            dirBullet1 = Quaternion.Euler(60, 0f, angle);
            clone1 = Instantiate(Bullet, Weapon.transform.position, dirBullet1) as GameObject;
            clone1.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed)+ Weapon.transform.TransformDirection(Vector3.right* Random.Range(5.0f, 18.0f));
            ///
            GameObject clone2;
            Quaternion dirBullet2;
            dirBullet2 = Quaternion.Euler(60, 0f, angle);
            clone2 = Instantiate(Bullet, Weapon.transform.position, dirBullet2) as GameObject;
            clone2.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed) + Weapon.transform.TransformDirection(Vector3.left* Random.Range(5.0f, 18.0f));
            ///
            GameObject clone3;
            Quaternion dirBullet3;
            dirBullet3 = Quaternion.Euler(60, 0f, angle);
            clone3 = Instantiate(Bullet, Weapon.transform.position, dirBullet3) as GameObject;
            clone3.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed) + Weapon.transform.TransformDirection(Vector3.left * Random.Range(22.0f, 30.0f));
            ///
            GameObject clone4;
            Quaternion dirBullet4;
            dirBullet4 = Quaternion.Euler(60, 0f, angle);
            clone4 = Instantiate(Bullet, Weapon.transform.position, dirBullet4) as GameObject;
            clone4.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed) + Weapon.transform.TransformDirection(Vector3.right * Random.Range(22.0f, 30.0f));
            //
            //transform.Translate(-Weapon.transform.TransformDirection(Vector3.right * 0.2f));

            StartCoroutine(DestroyBullet(clone));
            StartCoroutine(DestroyBullet(clone1));
            StartCoroutine(DestroyBullet(clone2));
            StartCoroutine(DestroyBullet(clone3));
            StartCoroutine(DestroyBullet(clone4));
            StartCoroutine(CDShoot(0.5f));
        }
    }

    void ShootMiniGun()
    {
        if (canshoot == true && ammoMG >0)
        {
            ammoMG -= 1;
            canshoot = false;
            GameObject clone;
            Quaternion dirBullet;
            dirBullet = Quaternion.Euler(60, 0f, angle);
            clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
            clone.GetComponent<Rigidbody>().velocity = Weapon.transform.TransformDirection(Vector3.up * bulletspeed) + Weapon.transform.TransformDirection(Vector3.right * Random.Range(-5.0f, 5.0f));
            //transform.Translate(-Weapon.transform.TransformDirection(Vector3.up * 1));
            StartCoroutine(DestroyBullet(clone));
            StartCoroutine(CDShoot(0.1f));
        }
    }

    IEnumerator DestroyBullet (GameObject bullet)
    {
        yield return new WaitForSeconds(1);
        Destroy(bullet);
    }

    IEnumerator CDShoot (float wait)
    {
        yield return new WaitForSeconds(wait);
        canshoot = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            life -= 1;
        }

        if (other.gameObject.tag =="Plank")
        {
            plank += 1;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "AmmoPistol")
        {
            ammoPistol += 10;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "AmmoSG")
        {
            ammoAR += 5;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "AmmoMG")
        {
            ammoMG += 25;
            Destroy(other.gameObject);
        }
    }
}
