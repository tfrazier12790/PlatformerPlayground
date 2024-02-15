using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GameController : MonoBehaviour
{
    [SerializeField] private bool gameStart = false;
    [SerializeField] private bool paused = true;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameStartScreen;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameStart)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
            }

            if (paused)
            {
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            }
        }
    }
    public bool GetPaused()
    {
        return paused;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GameStart()
    {
        gameStartScreen.SetActive(false);
        gameStart = true;
        paused = false;
    }
    public void GameEnd()
    {
        paused = true;
        gameStart = false;
        Time.timeScale = 0;
    }
}
