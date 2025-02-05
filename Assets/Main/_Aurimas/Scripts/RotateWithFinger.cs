using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithFinger : MonoBehaviour
{
    private float rotationSpeed = 5f;
    private Vector2 touchStartPos;

    void Update()
    {
        // Check for touch input
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // Check for touch phase
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Record the starting position of the touch
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    // Calculate the delta position of the touch
                    Vector2 touchDelta = touch.position - touchStartPos;

                    // Rotate the parent object based on touch delta
                    float rotationAmount = touchDelta.x * rotationSpeed * Time.deltaTime;
                    transform.Rotate(Vector3.up, rotationAmount, Space.World);

                    // Update the starting position for the next frame
                    touchStartPos = touch.position;
                    break;
            }
        }
    }
}
