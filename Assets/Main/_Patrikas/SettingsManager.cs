using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public int currentSettingIndex = 0;
    public GameObject[] settingsOptions;
    public Button previousButton;
    public Button nextButton;
    public TextMeshProUGUI firstButtonText;
    private bool isFirstOptionSelected = false; // Flag to check if the first option has been interacted with
    

    void Start()
    {
        // Initially, disable both buttons until the first option is selected
        previousButton.interactable = false;
        nextButton.interactable = false;
    }

    public void FirstOptionSelected()
    {
        if (!isFirstOptionSelected)
        {
            isFirstOptionSelected = true;
            nextButton.interactable = true;
            firstButtonText.text = "Unlock Position";
        }
        else
        {
            isFirstOptionSelected = false;
            nextButton.interactable = false;
            firstButtonText.text = "Lock Position";
        }
    }

    public void GoToNextSetting()
    {
        if (!isFirstOptionSelected && currentSettingIndex == 0)
        {
            // Require the first option to be selected before continuing
            return;
        }

        settingsOptions[currentSettingIndex].SetActive(false);
        currentSettingIndex++;
        currentSettingIndex = Mathf.Min(currentSettingIndex, settingsOptions.Length - 1);
        settingsOptions[currentSettingIndex].SetActive(true);
        UpdateButtonStates();
    }

    public void GoToPreviousSetting()
    {
        settingsOptions[currentSettingIndex].SetActive(false);
        currentSettingIndex--;
        currentSettingIndex = Mathf.Max(currentSettingIndex, 0);
        settingsOptions[currentSettingIndex].SetActive(true);
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        previousButton.interactable = currentSettingIndex > 0;
        nextButton.interactable = currentSettingIndex < settingsOptions.Length - 1 && isFirstOptionSelected;
    }

    public void PlayDanceAnimation(string animationName)
    {
        DataManager.Instance.GetSelectedDressPrefab().GetComponentInChildren<MannequinAnimationManager>().PlayAnimation(animationName);
    }
}