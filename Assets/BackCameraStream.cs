using UnityEngine;
using UnityEngine.UI;

public class BackCameraStream : MonoBehaviour
{
    [SerializeField]
    public RawImage rawImage; // UI RawImage to display the back camera stream
    
    private WebCamTexture webCamTexture;

    void Start()
    {
        // Find the back camera
        string backCamName = "";
        foreach (var device in WebCamTexture.devices)
        {
            if (!device.isFrontFacing)
            {
                backCamName = device.name;
                break;
            }
        }

        // If a back camera was found, start the WebCamTexture
        if (!string.IsNullOrEmpty(backCamName))
        {
            webCamTexture = new WebCamTexture(backCamName);
            rawImage.texture = webCamTexture;
            webCamTexture.Play();
        }
        else
        {
            Debug.LogError("No back camera found!");
        }
    }

    void OnDestroy()
    {
        // Stop the WebCamTexture when the object is destroyed
        if (webCamTexture != null)
        {
            Debug.Log("We are black");
            webCamTexture.Stop();
        }
    }
}