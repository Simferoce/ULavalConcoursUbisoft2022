using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class ClassSelection : MonoBehaviour
{
    [SerializeField] private ToggleGroup _group = null;
    [SerializeField] private Toggle _isStoryMode = null;

    public void ChoixClasse()
    {
        SceneManager.LoadScene("level");
        MenuPause.GameIsPaused = false;
    }

    public void OnStartGame()
    {
        Toggle toggle = _group.ActiveToggles().FirstOrDefault();
        if(toggle != null)
        {
            GameManager.Instance.Class = toggle.GetComponent<ClassToggleUI>().Class;
            GameManager.Instance.IsStoryMode = _isStoryMode.isOn;
            SceneManager.LoadScene("level");
            MenuPause.GameIsPaused = false;
        }
    }
}
