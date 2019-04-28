using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedEnemy : MonoBehaviour
{
    private Transform playerPos;
    private float speed; //get this from Enemy.cs

    private int randomSpot;
    private Enemy enemyScript;
    public GameObject enemy;

    Animator enemyAnim;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
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
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);



    
    }
}
