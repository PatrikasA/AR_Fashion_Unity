using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothingSelectionWindow : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(() =>
        {
            DataManager.Instance.ChangeWindow(2);
            DataManager.Instance.LoadSelectedDressIndex();
            Loader.Load(Loader.Scene.ClothingScene);
        });

        backButton.onClick.AddListener(() =>
        {
            DataManager.Instance.ChangeWindow(5);
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }
}
