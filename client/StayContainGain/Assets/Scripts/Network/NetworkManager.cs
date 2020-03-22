using UnityEngine;
using UnityEditor;
using System.Collections;
using Models;
using Proyecto26;

using UnityEngine.Networking;

[InitializeOnLoad]
public class NetworkManager : MonoBehaviour
{


    private readonly string basepath = "http://3.122.190.255/api/users/";
    private string bearerToken = "";
    private RequestHelper currentRequest;

    private static NetworkManager instance = null;

    public static NetworkManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            instance = new NetworkManager();
            return instance;

        }
    }

    public NetworkManager()
    {
        RestClient.DefaultRequestHeaders["Content-Type"] = "application/json";
    }

    public RSG.IPromise<bool> Register(RLoginData loginData)
    {



        currentRequest = new RequestHelper
        {
            Uri = basepath + "register",
            Body = loginData,
            EnableDebug = true
        };

        return new RSG.Promise<bool>((resolve, reject) =>
        {
            RestClient.Post<Post>(currentRequest).Then((arg) => resolve(true)).Catch((obj) => reject(obj));
        }
        );

    }
    // Use this for initialization

    public RSG.IPromise<RUserRespnse> Authenticate(RLoginData loginData)
    {
        currentRequest = new RequestHelper
        {
            Uri = basepath + "authenticate",
            Body = loginData,
            EnableDebug = true
        };
        return new RSG.Promise<RUserRespnse>((resolve, reject) =>
        {
            RestClient.Post<RAuthResponse>(currentRequest).Then((arg) => {
                this.bearerToken = arg.token;
                resolve(arg); 
            }).Catch((obj) => reject(obj));
        });
    }
}
