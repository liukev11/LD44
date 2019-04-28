using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Boss : MonoBehaviour
{
    public Transform targetObj;
    public Transform moveLeft;
    public Transform moveRight;

    public GameObject lc;
    private LevelChanger lcScript;

    public Transform rock1;
    public Transform rock2;
    public Transform rock3;

    public GameObject rockObject;
    public GameObject grunt;
    public GameObject specialGrunt;
    
    public CinemachineVirtualCamera VirtualCamera;
    
    private Animator anim;
    private bool goLeft;

    private bool spawningRocks;
    private bool startMoving;
    private float iterate;
    private float speed;
    private int state;
    private bool audioIsPlaying;
    private string soundName;
    private bool spawningGrunts;

    private bool start_;

    public bool isDead;

    public static Boss bossClass;

    // Start is called before the first frame update
    void Start()
    {
        bossClass = this;
        //on boss fight start : do move towards
        startMoving = true;
        speed = 4.0f;
        // states: 0 = moving left and right | 1 = summon monsters | 2 = spawn falling boulders
        state = 99; // start off with no state
        anim = GetComponent<Animator>();
        StartCoroutine("StateChange");
        isDead = false;
        lcScript = lc.GetComponent<LevelChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        //zoom out camera
        if (VirtualCamera.m_Lens.OrthographicSize < 5.5f)
        {
            VirtualCamera.m_Lens.OrthographicSize += 0.01f;
        }
        
        if (startMoving)
        {
            iterate = speed * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, targetObj.position, iterate);
            if (this.gameObject.transform.position == targetObj.position)
            {
                //end moving up
                startMoving = false;
                state = 0;
                
            }
        }

        //startMoving is FALSE
        



        

        if (state == 0) //Moving left/right state 
        {
            //move left and right
            
            if (!start_)
            {
                Debug.Log("Move");
                goLeft = false;
                
            }
            if (this.gameObject.transform.position.x == moveRight.position.x)
            {
                //move left
                goLeft = true;
                start_ = true;
                
            }
            else if (this.gameObject.transform.position.x == moveLeft.position.x)
            {
                //move right
                goLeft = false;
                start_ = true;
                
            }

            if (goLeft)
            {
                iterate = speed * Time.deltaTime;
                this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, moveLeft.position, iterate);
            }

            if (!goLeft)
            {
                iterate = speed * Time.deltaTime;
                this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, moveRight.position, iterate);
            }
            
            
        }

        else if (state == 1) // Summon monsters state
        {
            soundName = "summon";
            //play sound effect
            PlaySound(soundName);
            SpawnGrunts();
        }

        else if (state == 2) // summon boulders
        {
            soundName = "summon";

            SpawnRocks();
            PlaySound(soundName);
        }
        else
        {
            //nothing
        }

        
        // IF BOSS DIES --> KILL BOSS!

        if (isDead)
        {
            FindObjectOfType<AudioManager>().Play("bossDie");
            StartCoroutine(BossDies());
            FindObjectOfType<AudioManager>().Play("hitmarker");
            StopAllCoroutines();
            Destroy(gameObject);
            
        }


    }

    private void SpawnRocks()
    {
        if (!spawningRocks)
        {
            Instantiate(rockObject, rock1.position, rock1.rotation);
            Instantiate(rockObject, rock2.position, rock2.rotation);
            Instantiate(rockObject, rock3.position, rock3.rotation);
            spawningRocks = true;
        }
    }

    private void SpawnGrunts()
    {
        if (!spawningGrunts)
        {
            Instantiate(grunt, rock1.position, rock1.rotation);
            Instantiate(grunt, rock2.position, rock2.rotation);
            Instantiate(specialGrunt, rock3.position, rock3.rotation);
            spawningGrunts = true;
        }
    }


    IEnumerator BossDies()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(3.0f);
        

    }

    IEnumerator StateChange()
    {
        while(1==1)
        {
            

            state = Random.Range(0,3);
            spawningRocks = false;
            spawningGrunts = false;
            audioIsPlaying = false;
            yield return new WaitForSeconds(Random.Range(4,8));
            
        }

    }

    private void PlaySound(string name)
    {
        if (!audioIsPlaying)
        {
            FindObjectOfType<AudioManager>().Play(soundName);
            anim.SetTrigger("summonMonster"); // do animation
            audioIsPlaying = true;
            

        }
    }
    
    public void SetDead(bool dead)
    
    {
        isDead = dead;    
    }

}
