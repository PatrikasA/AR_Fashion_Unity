using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] dresses;
    public int selectedDress = 0;

    public void NextDress()
    {
        dresses[selectedDress].SetActive(false);
        selectedDress = (selectedDress + 1) % dresses.Length;
        dresses[selectedDress].SetActive(true);
    }

    public void PreviousDress()
    {
        dresses[selectedDress].SetActive(false);
        selectedDress--;
        if (selectedDress < 0)
        {
            selectedDress += dresses.Length;
        }
        dresses[selectedDress].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedDress",selectedDress);
        //SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
