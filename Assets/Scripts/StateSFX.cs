using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSFX : MonoBehaviour
{

    public AudioSource transitionSFX;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!transitionSFX.isPlaying)
            {
                transitionSFX.timeSamples = GetComponent<AudioSource>().clip.samples;
                transitionSFX.pitch = 1;
                transitionSFX.Play();
            }
            transitionSFX.pitch = 1;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!transitionSFX.isPlaying)
            {
                transitionSFX.timeSamples = GetComponent<AudioSource>().clip.samples - 1;
                transitionSFX.pitch = -1.3f;
                transitionSFX.Play();
            }
            transitionSFX.pitch = -1.3f;
        }
    }

}
