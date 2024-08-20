using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void LoadScene() {
        SceneManager.LoadScene("Main");
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/GenUI_Select");
    }
}
