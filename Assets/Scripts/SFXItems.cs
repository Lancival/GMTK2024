using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SFXItems : MonoBehaviour
{
    private Fish fish;
    private Decoration dec;
    private bool isFish = false;
    private int fishSize = 0;

    //0 = organic, 1 = rock, 2 = wood
    private int decType = 0;

    private void Awake()
    {
        GetFishSize();
    }

    private void GetDecMaterial()
    {
        dec = GetComponent<Decoration>();
        string decMat = dec.Material;
        isFish = false;
        if (decType == 0)
        {
            UnityEngine.Debug.Log(decMat);
            decType = 0;
        }
        else if (decMat == "Wood")
        {
            decType = 2;
        }
        else if (decMat == "Rock")
        {
            decType = 1;
        }

    }

    private void GetFishSize()
    {
        fish = GetComponent<Fish>();
        if (fish != null)
        {
            isFish = true;

            if (fish.Space <= 3)
            {
                fishSize = 0;
            }
            else if (fish.Space < 5)
            {
                fishSize = 1;
            }
            else
            {
                fishSize = 2;
            }
        }
    }

    public void SFXPlaySelect()
    {

        if (isFish)
        {
            GetFishSize();
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fish_Select", "Size", fishSize);
        }
        else
        {
            GetDecMaterial();
            
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Dec_Select", "Material", decType);
        }
    }

    public void SFXPlayPlace()
    {
        if (isFish)
        {
            GetFishSize();
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fish_Place", "Size", fishSize);
        }
        else
        {
            GetDecMaterial();
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Dec_Place", "Material", decType);
        }
    }

    public void SFXPlayReturn()
    {
        if (isFish)
        {
            GetFishSize();
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fish_Return", "Size", fishSize);
        }
        else
        {
            GetDecMaterial();
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Dec_Return", "Material", decType);
        }
    }

    public void SFXPlayFishMove()
    {
        GetFishSize();
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fish/FishMove", "Size", fishSize);
    }

    public void SFXPlayLevelComplete()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/TankComplete");
    }



}
