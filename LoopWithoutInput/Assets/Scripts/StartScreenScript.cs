using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenScript : MonoBehaviour
{
    public static bool startScreenActive;
    public GameObject StartScreenCanvas;
    public GameObject GameController;
    public GameObject CountdownScript;
    public GameObject Lvl1;

    public GameObject boy;
    public GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {
        startScreenActive = true;
        Lvl1.SetActive(false);
        boy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        ghost.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startScreenActive)
        {
            StartScreenCanvas.SetActive(false);
            Lvl1.SetActive(true);
            GameController.SetActive(true);
            CountdownScript.SetActive(true);

        }

        if (startScreenActive && Input.GetKeyDown("space"))
        {
            Debug.Log("Space pressed");
            startScreenActive = false;
        }
    }
}
