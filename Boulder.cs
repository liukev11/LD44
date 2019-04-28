using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("boulderFall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //destroy object
        //maybe instantiate some particles here as well
        FindObjectOfType<AudioManager>().Play("boulderDrop");
        Destroy(gameObject);
        
    }
}
