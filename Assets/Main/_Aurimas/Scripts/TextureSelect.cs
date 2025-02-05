using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScaleSlider))]
public class TextureSelect : MonoBehaviour
{
    [SerializeField]
    private List<Texture> textures;
    private ScaleSlider scaleSlider;
    // Start is called before the first frame update
    void Start()
    {
        scaleSlider = GetComponent<ScaleSlider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectTexture(int textureIndex)
    {
        Material current = scaleSlider.GetSelectedMaterial();
        current.SetTexture("_MainTex", textures[textureIndex]);
    }
}
