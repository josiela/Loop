using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public static bool movement;
    public bool countdown;
    public int countdownTime;
    public Text countdownDisplay;

    // Start is called before the first frame update
    void Start()
    {
        movement = false;
        countdown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown)
        {
            StartCountdownMethod();
        }
    }

    public void StartCountdownMethod()
    {
        StartCoroutine(CountdownToStart());
        countdown = false;
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "Go!";

        movement = true;

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }
}
