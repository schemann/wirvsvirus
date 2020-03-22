using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public enum eMenu
    {
        None,
        Login,
        Register,
        Recover,
        Game,
        CharacterCreate
    }

    [SerializeField] private Menu _login;
    [SerializeField] private Menu _register;
    [SerializeField] private Menu _create;
    [SerializeField] private Menu _gameMain;

    private Menu _activeMenu;

    void Start()
    {
        _login.Activate();
        _register.Deactivate();
        _create.Deactivate();
        _gameMain.Deactivate();

        _activeMenu = _login;
    }

    public void SwitchMenu(eMenu menu)
    {
        float fadeTime = 0.2f;
        switch (menu)
        {
            case eMenu.Login:
                _activeMenu.Deactivate(fadeTime);
                _login.Activate(fadeTime);
                _activeMenu = _login;
                break;
            case eMenu.Register:
                _activeMenu.Deactivate(fadeTime);
                _register.Activate(fadeTime);
                _activeMenu = _register;
                break;
            case eMenu.Recover:
                _activeMenu.Deactivate(fadeTime);
                break;
            case eMenu.CharacterCreate:
                _activeMenu.Deactivate(fadeTime);
                _create.Activate(fadeTime);
                _activeMenu = _create;
                break;
            case eMenu.Game:
                _activeMenu.Deactivate(fadeTime);
                _gameMain.Activate(fadeTime);
                _activeMenu = _gameMain;
                break;
        }
    }
}
