using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonInteraction : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Button button;

    [SerializeField] private Image innerVisualImage;
    [SerializeField] private Sprite selectedButtonSprite;
    [SerializeField] private Sprite idleButtonSprite;
    [SerializeField] private GameObject Dress;
    [SerializeField] private int index;
    [SerializeField] private bool save = false;

    [SerializeField] private bool isClothesSelectionButton = false;
    [SerializeField] private ClothesWearAnimation clothesWearAnimation;

    ClothingManagementWindow clothingManagementWindow;

    private void Awake()
    {
        clothingManagementWindow = GetComponentInParent<ClothingManagementWindow>();
    }

    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            if (save)
            {
                DataManager.Instance.SaveSelectedDressIndex(index);
                //Debug.Log(index);
            }

            if (isClothesSelectionButton)
            {
                clothesWearAnimation.SetCurrentDress(DressList.Instance.GetDressesList()[index].gameObject);
            }
        });
    }

    public void OnSelect(BaseEventData eventData)
    {
        clothingManagementWindow.SetOtherButtonsInteractable(this);
        //Dress.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        
       // Dress.SetActive(false);
    }

    public void ButtonSelected()
    {
        innerVisualImage.sprite = selectedButtonSprite;
    }

    public void ButtonDeselected()
    {
        innerVisualImage.sprite = idleButtonSprite;
    }
}
