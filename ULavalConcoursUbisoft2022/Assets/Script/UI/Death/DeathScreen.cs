using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private Player player = null;
    private Health health = null;

    [SerializeField] private UnityEvent _onOpenEndMenu = null;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();

        health = player.GetComponentInChildren<Health>();
        if (!GameManager.Instance.IsStoryMode)
        {
            health.OnDeath.AddListener(Health_OnDeath);
        }
    }

    private void Health_OnDeath(Health obj)
    {
        OpenEndMenu();
    }

    private void OnDestroy()
    {
        if (!GameManager.Instance.IsStoryMode)
        {
            health.OnDeath.RemoveListener(Health_OnDeath);
        }
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
