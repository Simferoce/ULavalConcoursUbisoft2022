using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private Player player = null;

    [SerializeField] private UnityEvent _onOpenEndMenu = null;

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
        OpenEndMenu();
    }

    private void OpenEndMenu()
    {
        _onOpenEndMenu?.Invoke();
        GameObject.FindObjectOfType<LevelManager>().IsGameInProgress = false;
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
