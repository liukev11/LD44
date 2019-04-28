using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject lc;

    private LevelChanger lcScript;

    // Start is called before the first frame update
    void Start()
    {
        lcScript = lc.GetComponent<LevelChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            lcScript.FadeToLevel("Level1");
        }
    }
}
