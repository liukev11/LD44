using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    private float iterate;
    private float speed;
    public Transform targetObj;
    private bool closeDoor;
    private bool audioPlayedClose;
    //move door up when player collides with it
    // Start is called before the first frame update
    void Start()
    {
        closeDoor = false;
        speed = 4.0f;
    }

    public void SetClose(bool doorStatus)
    {
        closeDoor = doorStatus;
    }

    // Update is called once per frame
    void Update()
    {
        if (closeDoor)
        {
            iterate = speed * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, targetObj.position, iterate);
            PlayClose();
            
        }
        
    }

    private void PlayClose()
    {
        if (!audioPlayedClose)
        {
            FindObjectOfType<AudioManager>().Play("doorClose");
            audioPlayedClose = true;
        }
        
    }

    
}
