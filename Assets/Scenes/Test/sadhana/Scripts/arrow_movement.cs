using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class arrow_movement : MonoBehaviour
{
    Rigidbody2D Rb;
    
    public float neutralSpeed;
    public float fastSpeed;
    public float slowSpeed;
    public float statusTimeInSeconds;
    float currentSpeed;
    float MovementX;
    float MovementY;

    private Animator anim;
    private AudioSource audioS;
    public GameObject headerText;
    
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        MovementX = 0;
        MovementY = 0;
        currentSpeed = neutralSpeed;

        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        StopAllCoroutines();
        if (collision.gameObject.tag == "SpeedSquare") {
            StartCoroutine(TempSpeedBuff(statusTimeInSeconds));
        } else if (collision.gameObject.tag == "SlowSquare") {
            StartCoroutine(TempSlowDebuff(statusTimeInSeconds));
        } else if (collision.gameObject.tag == "End") {
            Debug.Log("Player 1 Wins!");
            //TODO: Delete this and instead go back to the board
            returnToMenu();

        }
    }

    private void returnToMenu()
    {
        StartCoroutine("goToMenu");
    }

    IEnumerator goToMenu()
    {
        headerText.GetComponent<TextMeshProUGUI>().text = "PLAYER 1 WINS! (+5 coins)";
        headerText.SetActive(true);
        audioS.Play();
        PlayerPrefs.SetInt("P1Score", PlayerPrefs.GetInt("P1Score") + 5);
        yield return new WaitForSeconds(3f);
        // TODO: Add a way for players to return back to the board
        EventManager em = FindObjectOfType<EventManager>();
        em.LoadBoardMapTrigger();
    }

    IEnumerator TempSpeedBuff(float waitTime) {
        currentSpeed = fastSpeed;
        yield return new WaitForSeconds(waitTime);
        currentSpeed = neutralSpeed;
    }

    IEnumerator TempSlowDebuff(float waitTime) {
        currentSpeed = slowSpeed;
        yield return new WaitForSeconds(waitTime);
        currentSpeed = neutralSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Rb.velocity = new Vector2(MovementX * currentSpeed, MovementY * currentSpeed);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovementY = 1;
            anim.SetBool("Moving", true);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovementY = -1;
            anim.SetBool("Moving", true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovementX = 1;
            anim.SetBool("Moving", true);
            anim.SetBool("Right", true);
            anim.SetBool("Left", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovementX = -1;
            anim.SetBool("Moving", true);
            anim.SetBool("Left", true);
            anim.SetBool("Right", false);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) 
        {
            MovementY = 0;
            
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            MovementX = 0;
            anim.SetBool("Moving", false);
        }

        
    }
}
