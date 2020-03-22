using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAccountLogin : Menu
{

    public void OnLoginClicked()
    {
        NetworkManager network = NetworkManager.Instance;
        network.Authenticate(new RLoginData
        {
            username = "test",
            password = "test"
        }).Then((arg) => Debug.Log(arg)).Catch((arg) => Debug.Log(arg));
    }

    public void OnRegisterClicked()
    {
        _mainMenu.SwitchMenu(MainMenu.eMenu.Register);
    }

}
