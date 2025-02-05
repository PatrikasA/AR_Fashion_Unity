using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureSelectionWindow : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backButton;

    [SerializeField] private Button texturesButton;
    [SerializeField] private Button colorsButton;

    private void Awake()
    {
        if (DataManager.Instance.ShouldShowQRTowardsARScene())
        {
            continueButton.onClick.AddListener(() =>
            {
                DataManager.Instance.ChangeWindow(0);
                Loader.Load(Loader.Scene.QRScene);
            });
        }
        else
        {
            continueButton.onClick.AddListener(() =>
            {
                DataManager.Instance.ChangeWindow(3);
                Loader.Load(Loader.Scene.ARScene);
            });
        }

        backButton.onClick.AddListener(() =>
        {
            DataManager.Instance.ChangeWindow(1);
            Loader.Load(Loader.Scene.ClothingScene);
        });

        texturesButton.onClick.AddListener(() =>
        {
            texturesButton.transform.localScale = new Vector2(1.2f, 1.2f);
            colorsButton.transform.localScale = new Vector2(1f, 1f);
            texturesButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
            colorsButton.GetComponent<Image>().color = Color.white;
        });

        colorsButton.onClick.AddListener(() =>
        {
            colorsButton.transform.localScale = new Vector2(1.2f, 1.2f);
            texturesButton.transform.localScale = new Vector2(1f, 1f);
            texturesButton.GetComponent<Image>().color = Color.white;
            colorsButton.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
        });
    }
}
