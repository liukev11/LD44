using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float speed = 20f;
    public float bulletLifetime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = transform.right * speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        //destroy bullet after travelling/existing for a certain amount of time
        StartCoroutine(DestroyBullet(bulletLifetime));
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //destroy bullet, and instantiate particle effect
        if (hitInfo.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
        
        Destroy(gameObject);
        
    }

    IEnumerator DestroyBullet(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
