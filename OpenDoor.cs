using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private float iterate;
    private float speed;
    public Transform targetObj;
    private bool triggerOpen;
    private bool alreadyOpen;
    private bool audioPlayedOpen;
    private bool audioPlayedClose;
    public Transform originalPosition;
    //move door up when player collides with it
    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        triggerOpen = false;
        alreadyOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerOpen)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, targetObj.position, iterate);
            alreadyOpen = true;
            PlayOpen();
            
        }

        if (!triggerOpen && alreadyOpen)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, originalPosition.position, iterate);
            PlayClose();
        }
        
    }

    private void PlayOpen()
    {
        if (!audioPlayedOpen)
        {
            FindObjectOfType<AudioManager>().Play("doorOpen");
            audioPlayedOpen = true;
            audioPlayedClose = false;
        }
        
    }

    private void PlayClose()
    {
        if (!audioPlayedClose)
        {
            FindObjectOfType<AudioManager>().Play("doorClose");
            audioPlayedClose = true;
            audioPlayedOpen = false;
        }
        
    }

    public void ActiveWeight()
    {
        triggerOpen = true;
        iterate = speed * Time.deltaTime;
        
    }

    public void InactiveWeight()
    {
        triggerOpen = false;
        iterate = speed * Time.deltaTime;
        
    }
    
}
