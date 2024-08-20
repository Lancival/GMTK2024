using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXItems : MonoBehaviour
{
    private Fish fish;
    private Decoration dec;
    private bool isFish = false;
    private int fishSize = 0;

    //0 = organic, 1 = rock, 2 = wood
    private int decType = 0;

    void Awake()
    {
        fish = GetComponent<Fish>();
        if (fish != null)
        {
            isFish = true;

            if (fish.Space <= 3)
            {
                fishSize = 0;
            }
            else if (fish.Space <= 5)
            {
                fishSize = 1;
            }
            else
            {
                fishSize = 2;
            }

        }
        else
        {
            dec = GetComponent<Decoration>();
            isFish = false;

            if (dec.Type == DecoType.Organic)
            {
                decType = 0;
            }
            else if (dec.Type == DecoType.Wood)
            {
                decType = 2;
            }
            else
            {
                decType = 1;
            }
        }
    }

    public void SFXPlaySelect()
    {
        if (isFish)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fish_Select", "Size", fishSize);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Dec_Select", "Material", decType);
        }
    }

    public void SFXPlayPlace()
    {
        if (isFish)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fish_Place", "Size", fishSize);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Dec_Place", "Material", decType);
        }
    }

    public void SFXPlayReturn()
    {
        if (isFish)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Fish_Return", "Size", fishSize);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Dec_Return", "Material", decType);
        }
    }



}
