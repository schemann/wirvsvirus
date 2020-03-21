using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAccountLogin : Menu
{

    public void OnLoginClicked()
    {

    }

    public void OnRegisterClicked()
    {
        _mainMenu.SwitchMenu(MainMenu.eMenu.Register);
    }

}
