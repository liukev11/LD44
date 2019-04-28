using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public static float speed;

    private float dazedTime;
    public float startDazedTime;
    public Material red;
    private Material defaultMat;
    private float direction;
    private float currentPos;

    public GameObject goldCoin;
    public Transform spawnPoint;
    public Transform deathSpawnPoint;
    public GameObject coinParticles;


    private Animator anim;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        defaultMat = sr.material;
        currentPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        //stun time
        if (dazedTime <= 0)
        {
            speed = 0.5f;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            //explode w/ coin particles
            Instantiate(coinParticles, deathSpawnPoint.position, deathSpawnPoint.rotation);
            //play death noise
            Instantiate(goldCoin, spawnPoint.position, spawnPoint.rotation);
            Destroy(gameObject);
        }

        //determine which way the sprite is facing
        if (transform.position.x > currentPos)
        {
            //face right
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (transform.position.x < currentPos)
        {
            //face left
            transform.localRotation = Quaternion.Euler(0,180,0);
        }

        //update current position
        currentPos = transform.position.x;

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Bullet"))
        {
            Debug.Log("Hit");
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("hitmarker");
        sr.material = red;
        dazedTime = startDazedTime;
        health -= damage;

        Invoke(("ResetMaterial"), 0.2f);
        
        //any animations

    }

    void ResetMaterial()
    {
        sr.material = defaultMat;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
