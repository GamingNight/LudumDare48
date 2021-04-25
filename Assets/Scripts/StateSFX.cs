using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSFX : MonoBehaviour
{

    public AudioSource transitionSFX;


    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V))
        {
            if (!transitionSFX.isPlaying)
            {
                transitionSFX.Play();
            }
        }

       if (Input.GetKeyDown(KeyCode.C))
        {
            transitionSFX.pitch = 1;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            transitionSFX.pitch = -1;
        }

    }
}
