using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateWithMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject objectToRotate;
    [SerializeField] private float rotationSpeed = 200f;

    private bool isMousedOver = false;

    private void Start()
    {
        objectToRotate = DressList.Instance.gameObject;
    }

    void Update()
    {
        // Check for mouse input
        if (Input.GetMouseButton(0) && isMousedOver) // Assuming left mouse button for rotation
        {
            // Get the mouse movement along the X axis
            float mouseX = Input.GetAxis("Mouse X");

            // Rotate the object based on mouse movement
            float rotationAmount = mouseX * rotationSpeed * Time.deltaTime;
            objectToRotate.transform.Rotate(Vector3.up, rotationAmount, Space.World);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMousedOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMousedOver = false;
    }

}
