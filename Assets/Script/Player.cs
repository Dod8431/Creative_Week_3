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

    ///////// Weapons /////////
    bool CAC = true;
    bool Pistol = false;
    bool AR = false;
    bool MiniGun = false;

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
        Weapon.transform.rotation = Quaternion.Euler(0f, 0f, 90);
    }

    // Update is called once per frame
    void Update()
    {


        ///////////////// Move /////////////////////

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(x, y, 0) * speed);

        ///////////////// Aim /////////////////////

        float xaim = Input.GetAxis("Horizontal1");
        float yaim = Input.GetAxis("Vertical1");
        if (xaim != 0 && yaim != 0)
        {
            angle = Mathf.Atan2(xaim, yaim) * Mathf.Rad2Deg;
            Weapon.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
            lastAngle = Weapon.transform.rotation;
        }
        else
        {
            Weapon.transform.rotation = lastAngle;
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
            Debug.Log("cac");
            CACSelect = true;
            ARSelect = false;
            Pistolselect = false;
            MinigunSelect = false;
        }
        if (Input.GetAxis("VerticalPad") < 0)
            {
            Debug.Log("ar");
            CACSelect = false;
            ARSelect = true;
            Pistolselect = false;
            MinigunSelect = false;
        }

        if (Input.GetAxis("HorizontalPad") > 0)
            {
            Debug.Log("pistol");
            CACSelect = false;
            ARSelect = false;
            Pistolselect = true;
            MinigunSelect = false;
        }

        if (Input.GetAxis("HorizontalPad") < 0)
            {
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
            dirBullet = Quaternion.Euler(0f, 0f, angle);
            clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
            clone.GetComponent<Rigidbody2D>().velocity = Weapon.transform.TransformDirection(Vector3.right * bulletspeed);
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
            dirBullet = Quaternion.Euler(0f, 0f, angle);
            clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
            clone.GetComponent<Rigidbody2D>().velocity = Weapon.transform.TransformDirection(Vector3.right * bulletspeed);
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
            dirBullet = Quaternion.Euler(0f, 0f, angle);
            clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
            clone.GetComponent<Rigidbody2D>().velocity = Weapon.transform.TransformDirection(Vector3.right * bulletspeed);
            ///
            GameObject clone1;
            Quaternion dirBullet1;
            dirBullet1 = Quaternion.Euler(0f, 0f, angle);
            clone1 = Instantiate(Bullet, Weapon.transform.position, dirBullet1) as GameObject;
            clone1.GetComponent<Rigidbody2D>().velocity = Weapon.transform.TransformDirection(Vector3.right * bulletspeed)+ Weapon.transform.TransformDirection(Vector3.up);
            ///
            GameObject clone2;
            Quaternion dirBullet2;
            dirBullet2 = Quaternion.Euler(0f, 0f, angle);
            clone2 = Instantiate(Bullet, Weapon.transform.position, dirBullet2) as GameObject;
            clone2.GetComponent<Rigidbody2D>().velocity = Weapon.transform.TransformDirection(Vector3.right * bulletspeed) + Weapon.transform.TransformDirection(Vector3.down);
            //
            transform.Translate(-Weapon.transform.TransformDirection(Vector3.right * 0.2f));

            StartCoroutine(DestroyBullet(clone));
            StartCoroutine(DestroyBullet(clone1));
            StartCoroutine(DestroyBullet(clone2));
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
            dirBullet = Quaternion.Euler(0f, 0f, angle);
            clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
            clone.GetComponent<Rigidbody2D>().velocity = Weapon.transform.TransformDirection(Vector3.right * bulletspeed);
            transform.Translate(-Weapon.transform.TransformDirection(Vector3.right*0.1f));
            StartCoroutine(DestroyBullet(clone));
            StartCoroutine(CDShoot(0.1f));
        }
    }

    IEnumerator DestroyBullet (GameObject bullet)
    {
        yield return new WaitForSeconds(2);
        Destroy(bullet);
    }

    IEnumerator CDShoot (float wait)
    {
        yield return new WaitForSeconds(wait);
        canshoot = true;
    }
}
