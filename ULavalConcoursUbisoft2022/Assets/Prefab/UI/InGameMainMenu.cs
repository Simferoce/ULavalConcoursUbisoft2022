using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InGameMainMenu : MonoBehaviour
{
    [System.Serializable]
    public class Menu
    {
        [System.Serializable]
        public enum MenuType
        {
            SubmitScore = 0,
            Pause = 1,
            Awards = 2,
            EndGame = 3,
            Option = 4
        }

        public MenuType Type;
        public GameObject Left;
        public GameObject Right;

        public void Disable()
        {
            Left.SetActive(false);
            Right.SetActive(false);
        }

        public void Enable()
        {
            Left.SetActive(true);
            Right.SetActive(true);
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
}
