using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAccountRegister : Menu
{
    public void OnBackClicked()
    {
        _mainMenu.SwitchMenu(MainMenu.eMenu.Login);
    }

    public void OnRegisterClicked()
    {

    }
}
