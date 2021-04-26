using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSFX : MonoBehaviour
{
    public GlobalGameData globalGameData;

    private AudioSource transitionSFX;
    private int previousLayerIndex;

    void Start() {

        transitionSFX = GetComponent<AudioSource>();
        previousLayerIndex = 0;
    }

    void Update() {

        int currentLayerIndex = globalGameData.GetCurrentLayerIndex();
        if (currentLayerIndex > previousLayerIndex) { //deeper
            transitionSFX.time = 0;
            transitionSFX.pitch = 1;
            transitionSFX.Play();
        } else if (currentLayerIndex < previousLayerIndex) {  //shallower
            transitionSFX.time = 0;
            transitionSFX.pitch = 0.1f;
            transitionSFX.Play();
        }
        previousLayerIndex = currentLayerIndex;
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    if (!transitionSFX.isPlaying)
        //    {
        //        transitionSFX.timeSamples = GetComponent<AudioSource>().clip.samples;
        //        transitionSFX.pitch = 1;
        //        transitionSFX.Play();
        //    }
        //    transitionSFX.pitch = 1;
        //}

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    if (!transitionSFX.isPlaying)
        //    {
        //        transitionSFX.timeSamples = GetComponent<AudioSource>().clip.samples - 1;
        //        transitionSFX.pitch = -1.3f;
        //        transitionSFX.Play();
        //    }
        //    transitionSFX.pitch = -1.3f;
        //}
    }

}
