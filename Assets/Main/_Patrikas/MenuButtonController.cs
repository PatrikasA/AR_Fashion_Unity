using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(() =>
        {
            DataManager.Instance.ChangeWindow(0);
            Loader.Load(Loader.Scene.QRScene);
        });
        
        backButton.onClick.AddListener(() =>
        {
            DataManager.Instance.ChangeWindow(0);
            Loader.Load(Loader.Scene.ClothingScene);
        });
    }
}
