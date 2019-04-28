using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private Transform playerPos;
    private float speed; //get this from Enemy.cs
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int randomSpot;
    private Enemy enemyScript;
    public GameObject enemy;

    Animator enemyAnim;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        randomSpot = Random.Range(0, moveSpots.Length);
        enemyAnim = enemy.GetComponent<Animator>();
        speed = Enemy.speed;
    }

    void Update()
    {
        //default move to random spots
        //get speed every frame
        speed = Enemy.speed;
        //do this if player is near grunt
        
        //animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        //do this if player is undetected by grunt
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }



    
    }
}
