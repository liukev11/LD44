using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClose : MonoBehaviour
{
    public GameObject door;
    public GameObject boss;

    private CloseDoor doorScript;

    // Start is called before the first frame update
    void Start()
    {
        doorScript = door.GetComponent<CloseDoor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //player runs into trigger point
            doorScript.SetClose(true); // close door behind
            //trigger boss fight. then destroy trigger
            boss.SetActive(true); // turn on boss object

            Destroy(gameObject);
        }
    }
}
