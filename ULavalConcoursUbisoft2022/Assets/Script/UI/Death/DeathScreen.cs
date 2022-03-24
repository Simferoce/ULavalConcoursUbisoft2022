using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private Player player = null;

    [SerializeField] private UnityEvent _onOpenEndMenu = null;
    [SerializeField] private float _delayBeforeEndMenu = 0.0f;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    public void OnPlayerDefeated()
    {
        OpenEndMenu();
    }

    public void GiveUp()
    {
        OpenEndMenu();
    }

    public void OnDeathBoss()
    {
        StartCoroutine(OpenMenuAfterDelay());
    }

    private IEnumerator OpenMenuAfterDelay()
    {
        yield return new WaitForSeconds(_delayBeforeEndMenu);
        OpenEndMenu();
    }

    private void OpenEndMenu()
    {
        _onOpenEndMenu?.Invoke();
        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
        if(levelManager != null)
        {
            levelManager.IsGameInProgress = false;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("menu+");
        Time.timeScale = 1;
    }

    public void Retry()
    {
        SceneManager.LoadScene("level");
        Time.timeScale = 1;
    }
}
