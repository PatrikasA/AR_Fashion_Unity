using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class DataManager : MonoBehaviour
{
    public enum WindowTypeEnum
    {
        CodeInput,
        SelectClothes_01,
        SelectTextures_02,
        ARMannequin_03,
        Video_04,
        MainMenu
    }

    public static DataManager Instance;

    [SerializeField] private string[] codeInputs;

    GameObject selectedDress;

    private WindowTypeEnum currentWindow;
    private WindowTypeEnum previousWindow;

    private int selectedDressIndex = -1;

    private bool gameFinished = false;

    private bool shouldShowTutorial = true;

    private bool shouldShowQRTowardsARScene = true;

    private bool shouldSkipAllQRInputs = false;

    public string[] CodeInputs
    {
        get { return codeInputs; }
        set { codeInputs = value; }
    }

    public WindowTypeEnum CurrentWindow
    {
        get { return currentWindow; }
        set { currentWindow = value; }
    }

    public WindowTypeEnum PreviousWindow
    {
        get { return previousWindow; }
        set { previousWindow = value; }
    }
    
    public bool GameFinished
    {
        get { return gameFinished; }
        set { gameFinished = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //Debug.Log(CurrentWindow);
    }

    public void ChangeWindow(int windowIndex)
    {
        previousWindow = currentWindow;
        currentWindow = (WindowTypeEnum)windowIndex;
        //Debug.Log("Previous window " + PreviousWindow);
        //Debug.Log("Current window" + currentWindow);
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
    }
    
    public void SaveSelectedDressIndex(int index)
    {
        selectedDressIndex = index;
    }
    
    public int LoadSelectedDressIndex()
    {
        return selectedDressIndex;
    }

    public int GetSelectedDressIndex()
    {
        return selectedDressIndex;
    }

    public void SetSelectedDressPrefab(GameObject dressPrefab)
    {
        selectedDress = dressPrefab;
    }

    public GameObject GetSelectedDressPrefab()
    {
        return selectedDress;
    }

    public void ChangeShouldShowTutorial(bool value)
    {
        shouldShowTutorial = value;
    }

    public bool ShouldTutorialBeShown()
    {
        return shouldShowTutorial;
    }

    public bool ShouldShowQRTowardsARScene()
    {
        return shouldShowQRTowardsARScene;
    }

    public void ChangeShouldShowQRTowarsARScene(bool value)
    {
        shouldShowQRTowardsARScene = value;
    }

    public bool ShouldWeSkipQR()
    {
        return shouldSkipAllQRInputs;
    }

    public void ChangeShouldSkipQR(bool value)
    {
        shouldSkipAllQRInputs = value;
    }
    
}
