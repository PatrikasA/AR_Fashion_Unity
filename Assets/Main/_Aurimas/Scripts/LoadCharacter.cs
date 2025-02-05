using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] dressPrefabs;
    public Transform spawnPoint;


    private void Start()
    {
        int selectedDress = PlayerPrefs.GetInt("selectedDress");
        GameObject prefab = dressPrefabs[selectedDress];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
