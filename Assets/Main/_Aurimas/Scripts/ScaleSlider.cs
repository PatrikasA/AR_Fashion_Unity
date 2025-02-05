using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScaleSlider : MonoBehaviour
{
    public Slider xScaleSlider; 
    public Slider yScaleSlider; 
    public SkinnedMeshRenderer dressRenderer;
    private float scaleSliderNumber;
    private int _materialIndex = 0;
    private Material currentMaterial = null;
    public List <Button> butts;
    private Dictionary<Button, Vector2> buttonToScaleValues;
    [SerializeField] private Button currentButton;

    private void Start()
    {
        buttonToScaleValues = new Dictionary<Button, Vector2>();
        dressRenderer = DressList.Instance.GetDressesList()[DataManager.Instance.GetSelectedDressIndex()].
            GetComponentInChildren<ColorChangeTag>().GetComponent<SkinnedMeshRenderer>();
        currentMaterial = dressRenderer.materials[_materialIndex];
    }

    public void SetCurrentButton(Button button)
    {
        currentButton = button;
        buttonToScaleValues.TryAdd(currentButton, new Vector2(1f,1f));
        ApplySliderValues(buttonToScaleValues[currentButton]);
    }

    public void SelectMaterial(int materialIndex)
    {
        dressRenderer = DressList.Instance.GetDressesList()[DataManager.Instance.GetSelectedDressIndex()].
            GetComponentInChildren<ColorChangeTag>().GetComponent<SkinnedMeshRenderer>();
        _materialIndex = materialIndex;
        currentMaterial = dressRenderer.materials[_materialIndex];
    }
    // Update is called once per frame
    void Update()
    {
        float xTiling = xScaleSlider.value;
        float yTiling = yScaleSlider.value;
        
        if(dressRenderer != null)
        {
            Vector2 textureScale = currentMaterial.mainTextureScale;
            textureScale.x = xTiling;
            textureScale.y = yTiling;
            currentMaterial.mainTextureScale = textureScale;
        }
    }

    public Material GetSelectedMaterial()
    {
        return currentMaterial;
    }
    public void SaveSliderValueToButton()
    {
        Vector2 currentSliderValues = new Vector2(xScaleSlider.value, yScaleSlider.value);
        buttonToScaleValues[currentButton] = currentSliderValues;
    }
    private void ApplySliderValues(Vector2 scaleValues)
    {
        if (dressRenderer != null && currentMaterial !=null)
        {
            //Vector2 textureScale = scaleValues;
            //dressRenderer.material.mainTextureScale = textureScale;
            currentMaterial.mainTextureScale = scaleValues;
            xScaleSlider.value = scaleValues.x;
            yScaleSlider.value = scaleValues.y;
        }
    }
}
