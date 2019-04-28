using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public GameObject player;
    private float fireRate = 0.3f;
    private float lastShot = 0.0f;
    private Animator anim;

    void Start()
    {
        anim = player.GetComponent<Animator>();
    }

    void Update()
    {
        
        //player has to hold down left shift to aim gun before shooting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //slow move speed, changing the speed value from Player script
            Player.speed = 2.0f;

            //go to aim animation
            anim.SetBool("isAiming", true);
            //decrease player speed

            if (Input.GetButtonDown("Fire1"))
            {
                //shoot animation
                
                Shoot();
            }
        }
        else
        {
            anim.SetBool("isAiming", false);
            //reset move speed to default (3)
            Player.speed = 3.0f;
        }

    }




    void Shoot()
    {
        if (Time.time > fireRate + lastShot)
        {
            anim.SetTrigger("shoot");
            FindObjectOfType<AudioManager>().Play("bullet");
            Instantiate(bullet, firePoint.position, firePoint.rotation); //spawn bullet(s)
            lastShot = Time.time;
        }

    }
}
