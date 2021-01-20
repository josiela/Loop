using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndscreenButtons : MonoBehaviour
{
    public Button playAgainButton, quitButton;

    // Start is called before the first frame update
    void Start()
    {
        playAgainButton.onClick.AddListener(loadLevel);
        quitButton.onClick.AddListener(quitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void loadLevel()
    {
        SceneManager.LoadScene("test");
    }

    private void quitGame()
    {
        Application.Quit();
    } 
}
