using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [System.Serializable]
    public class Menu
    {
        [System.Serializable]
        public enum MenuType
        {
            Start = 0,
            Option = 1,
            Leaderboard = 2,
            Quit = 3
        }

        public MenuType Type;
        public GraphicRaycaster Raycaster;

        public void Disable()
        {
           Raycaster.enabled = false;
        }

        public void Enable()
        {
           Raycaster.enabled = true;
        }
    }

    [SerializeField] private List<Menu> _menus = new List<Menu>();
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _selectionCamera = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _menuCamera = null;

    public void DisableAll()
    {
        foreach (Menu menu in _menus)
        {
            menu.Disable();
        }
    }

    public void EnableAll()
    {
        foreach (Menu menu in _menus)
        {
            menu.Enable();
        }
    }

    public void Enable(int type)
    {
        _menus.FirstOrDefault(x => x.Type == (Menu.MenuType)type)?.Enable();
    }

    public void Disable(int type)
    {
        _menus.FirstOrDefault(x => x.Type == (Menu.MenuType)type).Disable();
    }
    
    public void SetCameraToSelection()
    {
        _selectionCamera.Priority = 1;
        _menuCamera.Priority = 0;
    }

    public void SetCameraToSpecificMenu()
    {
        _selectionCamera.Priority = 0;
        _menuCamera.Priority = 1;
    }

    public void Quitter()
    {

        Application.Quit();
    }
}
