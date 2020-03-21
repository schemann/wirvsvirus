﻿using System.Collections;
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
        Game
    }

    [SerializeField] private Menu _login;
    [SerializeField] private Menu _register;

    private Menu _activeMenu;

    void Awake()
    {
        _login.Activate();
        _register.Deactivate();

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
        }
    }
}
