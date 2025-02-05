using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class MainMenuWindow : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.Select();

        playButton.onClick.AddListener(() =>
        {
            DataManager.Instance.ChangeWindow(1);
            Loader.Load(Loader.Scene.ClothingScene);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }

    private void Start()
    {
        DataManager.Instance.CurrentWindow = DataManager.WindowTypeEnum.MainMenu;
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
    }
}
