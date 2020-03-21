using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class GpsPositionData
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }

    public float Accurracy { get; set; }

    public LocationServiceStatus Status { get; set; }

    public double DistanceTo(GpsPositionData gpsPositionData)
    {
        return DistanceTo(gpsPositionData.Latitude, gpsPositionData.Longitude);
    }

    public double DistanceTo( double lat2, double lon2, char unit = 'M')
    {
        double rlat1 = Math.PI * Latitude / 180;
        double rlat2 = Math.PI * lat2 / 180;
        double theta = Longitude - lon2;
        double rtheta = Math.PI * theta / 180;
        double dist =
            Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
            Math.Cos(rlat2) * Math.Cos(rtheta);
        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;

        switch (unit)
        {
            case 'M': //Meters -> default
                return dist * 1609.344;
            case 'K': //Kilometers -> default
                return dist * 1.609344;
        }

        return dist;
    }
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
        }
    }

    public PositionChangedDelegate OnPositionChanged;

    private void Start()
    {
#if PLATFORM_ANDROID 
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
#endif
        StartCoroutine("StartService");
    }

    private void Update()
    {
        if(Input.location.status == LocationServiceStatus.Running)
        {
            StartCoroutine("UpdateGPS");
        }
    }

    private void OnDestroy()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // Stop service if there is no need to query location updates continuously
            Input.location.Stop();
        }
    }

    private IEnumerator UpdateGPS()
    {

        // Access granted and location value could be retrieved
        Status = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.horizontalAccuracy;
        OnPositionChanged(new GpsPositionData()
        {
            Latitude = Input.location.lastData.latitude,
            Longitude = Input.location.lastData.longitude,
            Accurracy = Input.location.lastData.horizontalAccuracy,
            Status = LocationServiceStatus.Running
        });

        yield return null;

    }

    private IEnumerator StartService()
    {
        Status = "Init GPS...";

        // First, check if user has location service enabled
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Status = "Location service disabled by user"; 
            OnPositionChanged(new GpsPositionData()
            {
                Status = LocationServiceStatus.Failed
            });
            yield break;
        }

        // Start service before querying location
        Input.location.Start(10.0f, 3.0f);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            Status = "Wait for location";
            OnPositionChanged(new GpsPositionData()
            {
                Status = LocationServiceStatus.Initializing
            });
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            Status = "Timed out";
            OnPositionChanged(new GpsPositionData()
            {
                Status = LocationServiceStatus.Failed
            });
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Status = "Unable to determine device location";
            OnPositionChanged(new GpsPositionData()
            {
                Status = LocationServiceStatus.Failed
            });
            yield break;
        }
    }
}