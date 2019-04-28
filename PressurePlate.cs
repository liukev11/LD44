using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Sprite inactiveWeight;
    public Sprite activeWeight;
    public GameObject door;
    
    private OpenDoor doorScript;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = inactiveWeight;
        doorScript = door.GetComponent<OpenDoor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //check if either the player, or gold bar is on the object
        if (other.gameObject.CompareTag("GoldBar") || other.gameObject.CompareTag("Player"))
        {
            OpeningDoor();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //return to closed door state
        ClosingDoor();
    }

    private void OpeningDoor()
    {
        //pressure plate is currently being stepped on
        sr.sprite = activeWeight;
        //call open door script
        doorScript.ActiveWeight();
    }

    private void ClosingDoor()
    {
        sr.sprite = inactiveWeight;
        //call close door
        doorScript.InactiveWeight();
    }

    
}
