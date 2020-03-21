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
            textMesh.text = "Longitude: " + data.Longitude + " Latitude: " + data.Latitude + " Accurracy: " + data.Accurracy;
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
