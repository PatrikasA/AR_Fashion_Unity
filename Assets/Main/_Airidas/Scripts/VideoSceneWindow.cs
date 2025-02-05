using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VideoSceneWindow : MonoBehaviour
{
    [SerializeField] private Button videoButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button mainMenuButton;

    void Start()
    {
        DataManager.Instance.GameFinished = true;
        DataManager.Instance.ChangeShouldSkipQR(true);

        backButton.onClick.AddListener(() =>
        {
            DataManager.Instance.ChangeWindow(3);
            Loader.Load(Loader.Scene.ARScene);
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            DataManager.Instance.ChangeWindow(5);
            Destroy(DressList.Instance.gameObject);
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    void Update()
    {
        
    }
}
