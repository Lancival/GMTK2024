using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitbutton : MonoBehaviour
{
     public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        
        // If running in the Unity Editor, stop the play mode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
