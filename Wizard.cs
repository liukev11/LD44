using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    private Rigidbody2D rigid;
    private float addForce;

    public GameObject chain;

    private BoxCollider2D box2d;
    private MoveChain mc;

    // Start is called before the first frame update
    void Start()
    {
        addForce = 100.0f;
        rigid = GetComponent<Rigidbody2D>();
        mc = chain.GetComponent<MoveChain>();
        box2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Bullet"))
        {
            Die();
        }
    }

    private void Die()
    {
        //fly off stage
        Debug.Log("Hit");
        
        rigid.AddForce(transform.up * addForce);
        rigid.AddForce(transform.right * addForce);
        rigid.AddTorque(addForce * 0.25f);
        //activate chain
        mc.SetTriggerOpen(true);
        box2d.enabled = false;
        //play wizard death audio
        FindObjectOfType<AudioManager>().Play("hitmarker");
    }
}
