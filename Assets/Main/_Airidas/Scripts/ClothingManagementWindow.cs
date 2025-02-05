using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothingManagementWindow : MonoBehaviour
{
    [SerializeField] private ClothingSelectionWindow clothingSelectionWindow;
    [SerializeField] private TextureSelectionWindow textureSelectionWindow;
    [SerializeField] private GameObject ColoringSection;
    [SerializeField] private GameObject TexturingSection;

    [SerializeField] private ButtonInteraction[] ClothesButtons;
    [SerializeField] private ButtonInteraction[] ClothesPartsButtons;

    [SerializeField] private ButtonInteraction[] TexturesPartsButtons;

    private void Start()
    {
        if (DataManager.Instance.CurrentWindow == DataManager.WindowTypeEnum.SelectClothes_01)
        {
            clothingSelectionWindow.gameObject.SetActive(true);
            textureSelectionWindow.gameObject.SetActive(false);
        }
        else if (DataManager.Instance.CurrentWindow == DataManager.WindowTypeEnum.SelectTextures_02)
        {
            clothingSelectionWindow.gameObject.SetActive(false);
            textureSelectionWindow.gameObject.SetActive(true);
            ColoringSection.SetActive(true);
            TexturingSection.SetActive(false);
        }

        if (DataManager.Instance.GetSelectedDressIndex() != -1)
            ClothesButtons[DataManager.Instance.GetSelectedDressIndex()].ButtonSelected();
    }

    public void SetOtherButtonsInteractable(ButtonInteraction currentButton)
    {
        if (DataManager.Instance.CurrentWindow == DataManager.WindowTypeEnum.SelectClothes_01)
        {
            foreach (ButtonInteraction button in ClothesButtons)
            {
                button.ButtonDeselected();
            }
            currentButton.ButtonSelected();
        }
        else if (DataManager.Instance.CurrentWindow == DataManager.WindowTypeEnum.SelectTextures_02)
        {
            if (ColoringSection.activeInHierarchy)
            {
                foreach (ButtonInteraction button in ClothesPartsButtons)
                {
                    button.ButtonDeselected();
                }
                currentButton.ButtonSelected();
            }
            else if (TexturingSection.activeInHierarchy)
            {
                foreach (ButtonInteraction button in TexturesPartsButtons)
                {
                    button.ButtonDeselected();
                }
                currentButton.ButtonSelected();
            }
        }
    }

}
