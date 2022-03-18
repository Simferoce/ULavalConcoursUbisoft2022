using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{

    public static bool GameIsPaused = false;

    [SerializeField] private UnityEvent _onPause;
    [SerializeField] private UnityEvent _onResume;


    private bool _menuOpened = false;
    private LevelManager _levelManager = null;

    private void Start()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        _levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _levelManager.IsGameInProgress)
        {
            if (_menuOpened)
            {
                TriggerResume();
            }
            else
            {
                _onPause?.Invoke();
                Pause();
            }
        }
    }

    public void TriggerResume()
    {
        _onResume?.Invoke();
        _menuOpened = false;
    }

    public void Resume()
    {
        GameObject.FindObjectOfType<Player>().UnlockControl();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        GameObject.FindObjectOfType<Player>().LockControl();
        Time.timeScale = 0f;
        GameIsPaused = true;
        _menuOpened = true;
    }
}
