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
        NetworkManager network = NetworkManager.Instance;

        network.Register(new RLoginData
        {
            username ="test",
            password = "test"
        })
        .Then((arg) =>
        {
            Debug.Log(JsonUtility.ToJson(arg, true));
        }
                )
        .Catch((arg) => {
            Debug.Log(JsonUtility.ToJson(arg, true));
        }); 
    }
}
