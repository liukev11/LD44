using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //play falling beam noise
        FindObjectOfType<AudioManager>().Play("fallingBeam");
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            //stop falling beam
            FindObjectOfType<AudioManager>().Stop("fallingBeam");
            //play fallen beam noise
            FindObjectOfType<AudioManager>().Play("fallenBeam");
        }

    }
}
