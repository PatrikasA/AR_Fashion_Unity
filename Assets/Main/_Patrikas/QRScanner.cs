using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;
using ZXing;

public class QRScanner : MonoBehaviour
{
    private ARCameraManager _arCameraManager;
    private Texture2D _snap;
    [SerializeField] private TMP_InputField codeInputField;
    [SerializeField] private Button confirmButton;

    private XRCpuImage? _latestImage = null; 

    void Awake()
    {
        _arCameraManager = FindObjectOfType<ARCameraManager>();
        if (_arCameraManager == null)
        {
            Debug.LogError("ARCameraManager not found in the scene. Please add one to your AR Camera.");
        }
    }

    public void ScanForQRCode()
    {
        if (_arCameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            try
            {
                ProcessImage(image);
            }
            finally
            {
                image.Dispose(); 
            }
        }
    }

    private void ProcessImage(XRCpuImage image)
    {
        TextureFormat textureFormat = TextureFormat.ARGB32; 
        int width = image.width;
        int height = image.height;

        if (_snap == null || _snap.width != width || _snap.height != height)
        {
            _snap = new Texture2D(width, height, textureFormat, false);
        }

        XRCpuImage.ConversionParams conversionParams = new XRCpuImage.ConversionParams
        {
            inputRect = new RectInt(0, 0, width, height),
            outputDimensions = new Vector2Int(width, height),
            outputFormat = textureFormat, 
            transformation = XRCpuImage.Transformation.None 
        };

        var rawTextureData = _snap.GetRawTextureData<byte>();
        try
        {
            image.Convert(conversionParams, rawTextureData);
            _snap.Apply();
            DecodeQrFromTexture(_snap);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error converting image: {e.Message}");
        }
        finally
        {
            image.Dispose();
        }
    }
    
    private void DecodeQrFromTexture(Texture2D texture)
    {
        IBarcodeReader reader = new BarcodeReader
        {
            AutoRotate = true,
            Options = new ZXing.Common.DecodingOptions
            {
                TryInverted = true,
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat> { ZXing.BarcodeFormat.QR_CODE }
            }
        };
        var result = reader.Decode(texture.GetRawTextureData(), texture.width, texture.height, RGBLuminanceSource.BitmapFormat.ARGB32);
        if (result != null)
        {
            codeInputField.text = result.Text;
            confirmButton.onClick.Invoke();
        }
    }
}
