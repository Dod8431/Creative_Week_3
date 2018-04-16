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
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject clone;
        Quaternion dirBullet;
        dirBullet = Quaternion.Euler(0f, 0f, angle);
        clone = Instantiate(Bullet, Weapon.transform.position, dirBullet) as GameObject;
        clone.GetComponent<Rigidbody2D>().velocity = Weapon.transform.TransformDirection(Vector3.right * bulletspeed);
        StartCoroutine(DestroyBullet(clone));
    }

    IEnumerator DestroyBullet (GameObject bullet)
    {
        yield return new WaitForSeconds(2);
        Destroy(bullet);
    }
}
