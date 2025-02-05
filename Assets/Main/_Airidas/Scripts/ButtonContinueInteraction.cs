using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContinueInteraction : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        if (DataManager.Instance.GetSelectedDressIndex() != -1)
        {
            EnableButton();
        }
        else
        {
            DisableButton();
        }
    }

    public void DisableButton()
    {
        button.interactable = false;
    }

    public void EnableButton()
    {
        button.interactable = true;
    }
}
