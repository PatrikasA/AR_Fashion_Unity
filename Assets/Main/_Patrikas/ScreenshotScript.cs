using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public class ScreenshotScript : MonoBehaviour
{
    [SerializeField] private Image flashImage;
    [SerializeField] private GameObject menuButtons;

    public void CaptureScreenshot()
    {
        menuButtons.gameObject.SetActive(false);
        StartCoroutine(TakeScreenshotAndSave());
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Save the screenshot to Gallery/Photos
        string filename =
            $"{Application.productName}_Capture{{0}}_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        NativeGallery.SaveImageToGallery(ss, Application.productName + " Captures", filename);

        // Trigger the flash effect
        FlashEffect();
        menuButtons.gameObject.SetActive(true);
    }

    private IEnumerator FadeOutFlashImage()
    {
        // Duration of the fade in seconds
        float fadeDuration = 0.5f;
        float fadeStep = 0.1f; // How much to fade per step (adjust this value to make the fade smoother or faster)

        // Calculate how many steps the fade will take
        int steps = (int)(fadeDuration / fadeStep);
        float alphaChangePerStep = 1f / steps; // Calculate the change in alpha needed per step

        // Start with the image fully opaque
        flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 1);

        for (int i = 0; i < steps; i++)
        {
            // Reduce the alpha
            float newAlpha = flashImage.color.a - alphaChangePerStep;
            flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, newAlpha);

            // Wait for the next step
            yield return new WaitForSeconds(fadeStep);
        }

        // Ensure the flashImage is fully transparent and then hide it
        flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0);
        flashImage.gameObject.SetActive(false);
    }

    private void FlashEffect()
    {
        flashImage.gameObject.SetActive(true); // Make sure the image is visible
        StartCoroutine(FadeOutFlashImage()); // Start the fade out effect
    }

    private IEnumerator DisableFlashImageAfterFade()
    {
        yield return new WaitForSeconds(0.5f);
        flashImage.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0);
    }

}