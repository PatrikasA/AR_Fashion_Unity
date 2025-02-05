using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodeInputWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField codeInputField;
    [SerializeField] private TextMeshProUGUI errorMessageText;
    [SerializeField] private Button codeInputConfirmButton;
    [SerializeField] private Button codeScanBackButton;

    [SerializeField] private GameObject codeInputHolder;
    [SerializeField] private GameObject codeScanHolder;

    [SerializeField] private GameObject mannequinObject;

    /// TODO:
    /// Could potentially create a list to store all of the different windows in order to simplify ConfirmButton() and BackButton()
    ///

    private void Awake()
    {
        if (DataManager.Instance.ShouldWeSkipQR())
        {
            SkipQRQuickfix();
        }
        else
        {
            codeInputConfirmButton.onClick.AddListener(() => { ConfirmButton(); });
            codeScanBackButton.onClick.AddListener(() => { BackButton(); });
        }
    }

    public void SkipQRQuickfix()
    {
        if (DataManager.Instance.PreviousWindow == DataManager.WindowTypeEnum.SelectClothes_01) 
        {
            DataManager.Instance.ChangeWindow(2);
            DataManager.Instance.LoadSelectedDressIndex();
            Loader.Load(Loader.Scene.ClothingScene);
        }
        else if (DataManager.Instance.PreviousWindow == DataManager.WindowTypeEnum.SelectTextures_02)
        {
            DataManager.Instance.ChangeWindow(3);
            Loader.Load(Loader.Scene.ARScene);
        }
        else if (DataManager.Instance.PreviousWindow == DataManager.WindowTypeEnum.ARMannequin_03)
        {
            DataManager.Instance.ChangeWindow(4);
            Loader.Load(Loader.Scene.VideoScene);
        }
        else if (DataManager.Instance.PreviousWindow == DataManager.WindowTypeEnum.Video_04)
        {
            Debug.Log("FOURTH");
        }
    }

    public void ConfirmButton()
    {
        // Most likely will not need the 0 QR code input
        if (DataManager.Instance.PreviousWindow == DataManager.WindowTypeEnum.SelectClothes_01 && codeInputField.text == DataManager.Instance.CodeInputs[0]) 
        {
            //CloseCodeInputWindow(true);
            DataManager.Instance.ChangeWindow(2);
            DataManager.Instance.LoadSelectedDressIndex();
            Loader.Load(Loader.Scene.ClothingScene);
        }
        else if (DataManager.Instance.PreviousWindow == DataManager.WindowTypeEnum.SelectTextures_02 && codeInputField.text == DataManager.Instance.CodeInputs[1])
        {
            DataManager.Instance.ChangeWindow(3);
            Loader.Load(Loader.Scene.ARScene);
        }
        else if (DataManager.Instance.PreviousWindow == DataManager.WindowTypeEnum.ARMannequin_03 && codeInputField.text == DataManager.Instance.CodeInputs[2])
        {
            DataManager.Instance.ChangeWindow(4);
            Loader.Load(Loader.Scene.VideoScene);
        }
        else if (DataManager.Instance.PreviousWindow == DataManager.WindowTypeEnum.Video_04 && codeInputField.text == DataManager.Instance.CodeInputs[3])
        {
            Debug.Log("FOURTH");
        }
        else
        {
            errorMessageText.GetComponent<Animator>().Play("PopUp", 0, 0f);
            Debug.Log("Wrong input!");
        }
    }

    public void BackButton()
    {
        DataManager.Instance.ChangeWindow((int)DataManager.Instance.PreviousWindow);
        if (DataManager.Instance.CurrentWindow == DataManager.WindowTypeEnum.SelectClothes_01)
        {
            Loader.Load(Loader.Scene.ClothingScene);
            //CloseCodeInputWindow(true);
        }
        else if (DataManager.Instance.CurrentWindow == DataManager.WindowTypeEnum.SelectTextures_02)
        {
            Loader.Load(Loader.Scene.ClothingScene);
            //CloseCodeInputWindow(true);
        }
        else if (DataManager.Instance.CurrentWindow == DataManager.WindowTypeEnum.ARMannequin_03)
        {
            Loader.Load(Loader.Scene.ARScene);
            //CloseCodeInputWindow(true);
        }
    }

    void CloseCodeInputWindow(bool enableMannequin)
    {
        codeInputHolder.SetActive(false);
        codeScanHolder.SetActive(true);
        codeInputField.text = "";
        errorMessageText.GetComponent<Animator>().Play("Idle");
        mannequinObject.SetActive(enableMannequin);
        this.gameObject.SetActive(false);
    }
}
