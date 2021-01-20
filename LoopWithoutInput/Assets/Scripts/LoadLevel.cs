using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject Countdown;
    public GameObject boy;
    public GameObject ghost;
    public GameObject endscreen;
    public Text winnerText;
    public CircleCollider2D levelTrigger;

    public static bool startCountdown = false;
    public int winner;
    public bool triggerHit;
    public bool startLoading;
    private bool loadingLvl2;
    private bool loadingLvl3;


    public int boyWin = 0;
    public int ghostWin = 0;

    private Vector2 boyPositionLvl1 = new Vector2(-0.1962916f, 1.830842f);
    private Vector2 boyPositionLvl2 = new Vector2(0, 3.36f);
    private Vector2 boyPositionLvl3 = new Vector2(0, 4.86f);
    private Vector2 ghostPositionLvl1 = new Vector2(0.1962916f, -1.830842f);
    private Vector2 ghostPositionLvl2 = new Vector2(0, -3.35f);
    private Vector2 ghostPositionLvl3 = new Vector2(0, -4.82f);

    private Quaternion boyRotation = new Quaternion(0, 0, 0, 0);
    private Quaternion ghostRotation = new Quaternion(0, 0, 180, 0);

    // I think winanimation
    private bool colorAnimationEnabled;
    private float damping = 1;
    private bool winAnimation;
    public GameObject cyanBG;
    public GameObject orangeBG;
    public float winAnimationSpeed = 0.6f;
    public float maxAnimationRadius = 30;


    // Start is called before the first frame update
    void Start()
    {
        ResetPlayers(1);
        endscreen.SetActive(false);
        startLoading = false;
        triggerHit = false;
        colorAnimationEnabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //AdjustLevelTrigger();
        if (!CountdownLvl1.movement || winAnimation) {
            boy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            ghost.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        } else
        {
            boy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            ghost.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }

        if (winAnimation && colorAnimationEnabled)
        {
            if (winner == 1)
            {
                cyanBG.transform.localScale += new Vector3(winAnimationSpeed * damping, winAnimationSpeed * damping, 0);
                damping -= 0.01f;
            }
            else if (winner == 2)
            {
                orangeBG.transform.localScale += new Vector3(winAnimationSpeed * damping, winAnimationSpeed * damping, 0);
                damping -= 0.01f;
            }
            else
            {
                Debug.Log(winner);
                Debug.Log("Win error");
            }

            if (cyanBG.transform.localScale.x > maxAnimationRadius || orangeBG.transform.localScale.x > maxAnimationRadius)
            {
                damping = 1;
                cyanBG.transform.localScale = Vector3.zero;
                orangeBG.transform.localScale = Vector3.zero;
                StartCoroutine("WaitForSeconds");
                LoadNextScene();
                winAnimation = false;
            }
        }
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(2f);
    }

    private void LoadNextScene()
    {
        if (level1.activeSelf)
        {
            level1.SetActive(false);
            ResetPlayers(2);
            level2.SetActive(true);
        }
        else if (level2.activeSelf)
        {
            level2.SetActive(false);
            ResetPlayers(3);
            level3.SetActive(true); 
        }
        else if (level3.activeSelf)
        {
            PlayerMovement.movementActive = false;
            GhostMovement.movementActive = false;
            OpenEndscreen(boyWin, ghostWin);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        triggerHit = true;
        Debug.Log("Hit trigger by " + other.tag);
        if (other.tag == "Boy")
        {
            boyWin++;
            winner = 1;
        }
        else if (other.tag == "Ghost")
        {
            ghostWin++;
            winner = 2;
        }
        winAnimation = true;
    }

    /*private void AdjustLevelTrigger()
    {
        if (level1.activeSelf)
        {
            levelTrigger.transform.position = new Vector2(-0.02f, -0.02f);
        }
        else if (level2.activeSelf)
        {
            levelTrigger.transform.position = new Vector2(0.06f, -0.36f);
        }
        else if (level3.activeSelf)
        {
            levelTrigger.transform.position = new Vector2(0.2f, -0.17f);
        }
    }*/

    void OpenEndscreen(int boyWin, int ghostWin)
    {
        Debug.Log("In openEndscreen method");
        endscreen.SetActive(true);
        Debug.Log(endscreen.activeInHierarchy);

        if (boyWin > ghostWin)
        {
            winnerText.text = "Boy won!";
        } else
        {
            winnerText.text = "Ghost won!";
        }

        colorAnimationEnabled = false;
    }

    void ResetPlayers(int lvl)
    {
        switch (lvl)
        {
            case 1:
                boy.transform.position = boyPositionLvl1;
                ghost.transform.position = ghostPositionLvl1;
                //ghost.transform.position = ;
                break;
            case 2:
                boy.transform.position = boyPositionLvl2;
                ghost.transform.position = ghostPositionLvl2;
                break;
            case 3:
                boy.transform.position = boyPositionLvl3;
                ghost.transform.position = ghostPositionLvl3;
                break;
        }
        boy.transform.rotation = boyRotation;
        ghost.transform.rotation = ghostRotation;
    }
}