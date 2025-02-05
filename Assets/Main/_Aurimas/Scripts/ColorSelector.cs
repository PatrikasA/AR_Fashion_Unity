using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelector : MonoBehaviour
{
    [SerializeField] private Material[] material;
    [SerializeField] private Color[] selectableColors;
    [SerializeField] private Color startColor;
    [SerializeField] private GameObject colorGrid;
    private int _materialIndex = 0;

    private SkinnedMeshRenderer dressRenderer;

    private void Start()
    {
        colorGrid.SetActive(false);
    }

    public void SelectMaterial(int materialIndex)
    {
        dressRenderer = DressList.Instance.GetDressesList()[DataManager.Instance.GetSelectedDressIndex()].
            GetComponentInChildren<ColorChangeTag>().GetComponent<SkinnedMeshRenderer>();
        _materialIndex = materialIndex;
    }

    public void ChangeColor(int colorIndex)
    {
        dressRenderer.materials[_materialIndex].color = selectableColors[colorIndex];
    }
    
}
