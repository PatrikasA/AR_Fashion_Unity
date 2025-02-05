using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ClothingRotation : MonoBehaviour
{
    [SerializeField]
    ARFace userHead;

    Quaternion moveRotation;
    Vector3 lookPosition;

    private void Start()
    {
       
    }

    void Update()
    {

        //Quaternion worldRotation = transform.rotation;
        //Vector3 worldRotationEuler = worldRotation.eulerAngles;
        //transform.rotation = Quaternion.Euler(new Vector3(0, worldRotationEuler.y, 0));
        
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
