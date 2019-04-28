using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySong : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayMusic());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(0.11f);
        FindObjectOfType<AudioManager>().Play("playerSpawn");
        FindObjectOfType<AudioManager>().Play("soundtrack");
    }
}
