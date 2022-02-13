using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject canvas;
    // Update is called once per frame


    private void Start()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();

            }

        }
    }
     public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
     public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }
}
