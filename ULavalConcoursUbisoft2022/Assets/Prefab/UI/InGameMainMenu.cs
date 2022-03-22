using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InGameMainMenu : MonoBehaviour
{
    [System.Serializable]
    public class Menu
    {
        [System.Serializable]
        public enum MenuType
        {
            Pause = 0,
            Awards = 1,
            EndGame = 2,
        }

        public MenuType Type;
        public Animator Animator;
        public UnityEvent _onEnable;
        public UnityEvent _onDisable;

        public void Disable()
        {
            Animator.SetTrigger("Close");
            _onDisable?.Invoke();
        }

        public void Enable()
        {
            Animator.SetTrigger("Open");
            _onEnable?.Invoke();
        }
    }

    [SerializeField] private List<Menu> _menus = new List<Menu>();

    public void DisableAll()
    {
        foreach (Menu menu in _menus)
        {
            menu.Disable();
        }
    }

    public void Enable(int type)
    {
        _menus.FirstOrDefault(x => x.Type == (Menu.MenuType) type)?.Enable();
    }

    public void Disable(int type)
    {
        _menus.FirstOrDefault(x => x.Type == (Menu.MenuType)type).Disable();
    }

    private int _pause = 0;
    public void StackPause()
    {
        if(_pause == 0)
        {
            Time.timeScale = 1f;
            MenuPause.GameIsPaused = false;
        }
        _pause++;
    }

    public void UnstackPause()
    {
        _pause--;
        if (_pause == 0)
        {
            Time.timeScale = 0f;
            MenuPause.GameIsPaused = true;
        }
    }
}
