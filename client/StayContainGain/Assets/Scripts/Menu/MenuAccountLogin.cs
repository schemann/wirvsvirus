using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAccountLogin : Menu
{
    [SerializeField] private InputField _username;
    [SerializeField] private InputField _password;

    public void OnLoginClicked()
    {
        NetworkManager network = NetworkManager.Instance;
        network.Authenticate(new RLoginData
        {
            username = _username.text,
            password = _password.text
        }).Then((arg) =>
        {
            Debug.Log(arg);
        }).Catch((arg) => Debug.LogError(arg));
    }

    public void OnRegisterClicked()
    {
        _mainMenu.SwitchMenu(MainMenu.eMenu.Register);
    }

}
