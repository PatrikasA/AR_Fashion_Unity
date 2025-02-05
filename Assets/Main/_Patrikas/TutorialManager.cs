using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpro;
    [SerializeField] private ARPlaneManager _arPlaneManager;
    private bool HasBeenShown = false;
    void OnEnable()
    {
        if (DataManager.Instance.ShouldTutorialBeShown())
        {
            _arPlaneManager.planesChanged += OnPlanesChanged;
            StartCoroutine(EnableTextAfterDelay(2));
        }
        else
        {
            tmpro.gameObject.SetActive(false);
        }
        
        DataManager.Instance.ChangeShouldShowQRTowarsARScene(false);
    }

    void OnDisable()
    {
        _arPlaneManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        if(!HasBeenShown)
        {
            if (eventArgs.added != null && eventArgs.added.Count > 0)
            {
                HasBeenShown = true;
                DataManager.Instance.ChangeShouldShowTutorial(false);
                StartCoroutine(ChangeTextAfterDelay("Click the ground", 2));
            }
        }
    }
    public void ChangeDisplayText(string text)
    {
        tmpro.text = text;
    }
    
    IEnumerator ChangeTextAfterDelay(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeDisplayText(text);
        _arPlaneManager.planesChanged -= OnPlanesChanged;
    }
    
    IEnumerator EnableTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        tmpro.gameObject.SetActive(true);
    }
    
}
