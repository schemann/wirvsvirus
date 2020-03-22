using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAccountRegister : Menu
{
    [SerializeField] private InputField _username;
    [SerializeField] private InputField _password;

    public void OnBackClicked()
    {
        _mainMenu.SwitchMenu(MainMenu.eMenu.Login);
    }

    public void OnRegisterClicked()
    {
        NetworkManager network = NetworkManager.Instance;

        network.Register(new RLoginData
        {
            username = _username.text,
            password = _password.text
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
