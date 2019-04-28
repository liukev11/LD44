
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private string levelToLoad;

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeToLevel(string levelName)
    {
        levelToLoad = levelName;
        animator.SetTrigger("fadeOut");
        FindObjectOfType<AudioManager>().Play("start");
    }

    public void OnFadeComplete()
    {
        
        SceneManager.LoadScene(levelToLoad);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
