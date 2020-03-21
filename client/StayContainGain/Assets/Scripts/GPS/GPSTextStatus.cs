using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GPSTextStatus : MonoBehaviour
{
    [SerializeField]
    GPSPositionProvider GpsPositionProvider;

    private Text textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<Text>();

        if (GpsPositionProvider != null)
        {
            GpsPositionProvider.OnPositionChanged = ReceivePositionChanged;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ReceivePositionChanged(GpsPositionData data)
    {
        if (data.Status == LocationServiceStatus.Running)
        {
            var distance = data.DistanceTo(48.1230335, 11.5363742);
            textMesh.text = "Longitude: " + data.Longitude + " Latitude: " + data.Latitude + " Accurracy: " + data.Accurracy + " Distance: " + distance;
        } else if (data.Status == LocationServiceStatus.Initializing)
        {
            textMesh.text = "Waiting for GPS-Data";
        }
        else
        {
            textMesh.text = "Device or GPS Data not available";
        }
    }
}
