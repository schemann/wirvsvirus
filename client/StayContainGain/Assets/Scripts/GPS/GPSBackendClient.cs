using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using Models;
using UnityEngine;
using UnityEngine.Networking;

public class GPSBackendClient : MonoBehaviour
{
    [SerializeField]
    private GPSPositionProvider positionProvider;
    private RHomeBase lastGpsCoordinate;
    private RHomeBase homeBase;

    private string basePath = "http://3.122.190.255/api/users/";
    private RequestHelper currentRequest;

    public bool ValueAvailable { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        positionProvider.OnPositionChanged = ReceivePositionChanged;
    }

    void OnDisable()
    {
        positionProvider.OnPositionChanged = null;
    }


    public void SetHomeBase()
    {
        // We can add default query string params for all requests
        currentRequest = new RequestHelper
        {
            Uri = basePath + "setHomeBase?override=true",
            Headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZTc2NzVkMzM5OWNiODY3MTE2Nzc2NzQiLCJpYXQiOjE1ODQ4MjE3Mjh9.4H2UXZCfpHSustg6k-U9E98UlYKy4lH7aBhCnlfEdTU" }
            },
            Body = lastGpsCoordinate,
            EnableDebug = true
        };
        RestClient.Post<Post>(currentRequest)
        .Then(res =>
        {
            // And later we can clear the default query string params for all requests
            RestClient.ClearDefaultParams();
            homeBase = lastGpsCoordinate;

            this.LogMessage("Success", JsonUtility.ToJson(res, true));
        })
        .Catch(err => this.LogMessage("Error", err.Message));

    }

    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }

    public void GetHomeBase()
    {

    }

    private void ReceivePositionChanged(GpsPositionData data)
    {
        if (data.Status == LocationServiceStatus.Running)
        {
            ValueAvailable = true;
            lastGpsCoordinate = new RHomeBase() { latitude = data.Latitude, longitude = data.Longitude };
        }
        else
        {
            ValueAvailable = false;
        }

    }

}
