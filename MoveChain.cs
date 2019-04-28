using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChain : MonoBehaviour
{
    private float iterate;
    private float speed;
    public Transform targetObj;
    private bool triggerOpen;
    private bool playingAudio;
    public GameObject steelBeam;
    private Rigidbody2D rb;
    //move door up when player collides with it
    // Start is called before the first frame update
    void Start()
    {
        speed = 15.0f;
        rb = steelBeam.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerOpen)
        {
            moveUp();
            
        }
    }

    private void moveUp()
    {
        steelBeam.SetActive(true);
        iterate = speed * Time.deltaTime;
        //call this when the wizard is shot

        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, targetObj.position, iterate);
        playingAudio = false;
        if (!playingAudio)
        {
            PlaySound();
        }
        // turn on gravity on steel beam object
        rb.gravityScale = 0.8f;
    }

    public void SetTriggerOpen(bool status)
    {   
        
        triggerOpen = status;
    }

    private void PlaySound()
    {
        //play audio
        playingAudio = true;
    }
}
