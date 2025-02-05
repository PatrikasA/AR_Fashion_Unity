using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customize : MonoBehaviour
{

    public GameObject[] dresses;

    private int currentDress;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < dresses.Length; i++)
        {
            if (i == currentDress)
            {
                dresses[i].SetActive(true);
            }
            else
            {
                dresses[i].SetActive(false);
            }
        }
    }

    public void SwitchDress()
    {
        if (currentDress == dresses.Length - 1)
        {
            currentDress = 0;
        }
        else
        {
            currentDress++;
        }
    }
}
