using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXItems : MonoBehaviour
{
    private bool isFish = false;

    void Start()
    {
        if (GetComponent<Fish>() != null)
        {
            isFish = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SFXPlaySelect()
    {
        if (isFish)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fish_Select", "Size", 0);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Dec_Select", "Material", 0);
        }
    }

    public void SFXPlayPlace()
    {
        if (isFish)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fish_Place", "Size", 0);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Dec_Place", "Material", 0);
        }
    }

    public void SFXPlayReturn()
    {
        if (isFish)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fish_Return", "Size", 0);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Dec_Return", "Material", 0);
        }
    }



}
