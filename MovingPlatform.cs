using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float iterate;
    public float speed;
    public Transform origPoint;
    public Transform newPoint;    

    private bool starter;

    // Start is called before the first frame update
    void Start()
    {
        starter = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position == origPoint.position)
        {
            starter = true;

        }
        else if (this.gameObject.transform.position == newPoint.position)
        {
            starter = false;

        }
        if (starter)
        {
            iterate = speed * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, newPoint.position, iterate);
        }
        else if (!starter)
        {
            iterate = speed * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, origPoint.position, iterate);
        }
    }

}
