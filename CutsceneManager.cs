using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public GameObject cutscene1;
    public GameObject cutscene2;
    public GameObject cutscene3;
    public GameObject cutscene4;
    public GameObject cutscene5;
    public GameObject lc;

    private int sceneCounter;
    private LevelChanger lcScript;

    // Start is called before the first frame update
    void Start()
    {
        sceneCounter = 0;
        lcScript = lc.GetComponent<LevelChanger>();
        //start with cutscene1 enabled, and all others disabled
        cutscene1.SetActive(true);
        cutscene2.SetActive(false);
        cutscene3.SetActive(false);
        cutscene4.SetActive(false);
        cutscene5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            FindObjectOfType<AudioManager>().Play("next");
            NextScene();
        }
    }

    private void NextScene()
    {
        if (sceneCounter == 0)
        {
            cutscene1.SetActive(false);
            cutscene2.SetActive(true);
            sceneCounter += 1;
        }
        else if (sceneCounter == 1)
        {
            cutscene2.SetActive(false);
            cutscene3.SetActive(true);
            sceneCounter += 1;
        }
        else if (sceneCounter == 2)
        {
            cutscene3.SetActive(false);
            cutscene4.SetActive(true);
            sceneCounter += 1;
        }
        else if (sceneCounter == 3)
        {
            cutscene4.SetActive(false);
            cutscene5.SetActive(true);
            sceneCounter += 1;
        }
        else if (sceneCounter == 4)
        {
            lcScript.FadeToLevel("Instructions");
        }

    }
}
