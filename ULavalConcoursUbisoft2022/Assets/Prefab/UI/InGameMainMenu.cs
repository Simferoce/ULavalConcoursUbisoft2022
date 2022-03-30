using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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
    [SerializeField] private Camera _menuCamera = null;
    [SerializeField] private Volume _volume = null;
    [SerializeField] private float _lerpSpeed = 1.0f;

    private float _targetBloom = 1;
    private int _pause = 0;
    private Bloom _bloom = null;
    private float _lerpT = 0.0f;

    private void Awake()
    {
        _menuCamera.enabled = true;

        _volume.profile.TryGet<Bloom>(out _bloom);
    }

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


    public void StackPause()
    {
        _pause++;
        if (_pause > 0)
        {
            _targetBloom = 1;
            _lerpT = 0.0f;
            Time.timeScale = 0f;
            MenuPause.GameIsPaused = true;
            _menuCamera.enabled = true;

        }
    }

    public void UnstackPause()
    {
        _pause--;
        if (_pause <= 0)
        {
            _targetBloom = 9;
            _lerpT = 0.0f;
            Time.timeScale = 1f;
            MenuPause.GameIsPaused = false;
            _menuCamera.enabled = false;
        }
    }

    public void Update()
    {
        if(_lerpT > 1.0f) {
            _bloom.intensity.value = _targetBloom;
        }
        else
        {
            _bloom.intensity.value = Mathf.Lerp(_bloom.intensity.value, _targetBloom, _lerpT);
            _lerpT += 0.5f * Time.unscaledDeltaTime * _lerpSpeed;
        }
    }
}
