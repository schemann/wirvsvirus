using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GpsPositionData
{
    public bool Available { get; set; }
    public float HomeLattitude { get; set; }
    public float HomeLongitude { get; set; }
    public float CurrentLattitude { get; set; }
    public float CurrentLongitude { get; set; }
    public float DistanceToHome { get; set; }
    public float Accurracy { get; set; }

    public string Status { get; set; }
}

public delegate void PositionChangedDelegate(GpsPositionData data);

public class GPSPositionProvider : MonoBehaviour
{
    [SerializeField]
    private string _status;

    public string Status
    {
        get { return _status; }
        set
        {
            _status = value;
            UpdateTextField();
        }
    }

    public PositionChangedDelegate OnPositionChanged;

    private void Start()
    {
        StartCoroutine("StartService");
    }

    private void Update()
    {

    }

    private IEnumerator StartService()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            Status = "Location service disabled by user";
        yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            Status = "Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Status = "Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            Status = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    private void UpdateTextField()
    {
        OnPositionChanged(new GpsPositionData() { Status = Status });
    }
}