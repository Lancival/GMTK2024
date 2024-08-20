using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCutscene : MonoBehaviour
{
    public Canvas targetCanvas;  // Assign the Canvas you want to hide in the Inspector

    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Hide the canvas by disabling it
            if (targetCanvas != null)
            {
                targetCanvas.gameObject.SetActive(false);
            }
        }
    }
}