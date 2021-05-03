using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSFX : MonoBehaviour
{
    public GlobalGameDataSO globalGameData;

    private AudioSource transitionSFX;
    private int previousLayerIndex;
    private bool previousSwitchWasDeeper;

    void Start() {

        transitionSFX = GetComponent<AudioSource>();
        previousLayerIndex = 0;
        previousSwitchWasDeeper = false;
    }

    void Update() {

        int currentLayerIndex = globalGameData.GetCurrentLayerIndex();
        if (currentLayerIndex > previousLayerIndex) { //deeper
            transitionSFX.pitch = 1;
            if (!transitionSFX.isPlaying || previousSwitchWasDeeper) {
                transitionSFX.timeSamples = 0;
                transitionSFX.Play();
            }
            previousSwitchWasDeeper = true;
        } else if (currentLayerIndex < previousLayerIndex) {  //shallower
            transitionSFX.pitch = -1.3f;
            if (!transitionSFX.isPlaying || !previousSwitchWasDeeper) {
                transitionSFX.timeSamples = transitionSFX.clip.samples - 1;
                transitionSFX.Play();
            }
            previousSwitchWasDeeper = false;
        }
        previousLayerIndex = currentLayerIndex;
    }

}
