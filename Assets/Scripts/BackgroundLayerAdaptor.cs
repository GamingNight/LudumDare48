using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLayerAdaptor : MonoBehaviour
{

    public GlobalGameDataSO globalGameDate;

    private Animator[] allChildrenAnimators;
    // Start is called before the first frame update
    void Start() {

        allChildrenAnimators = GetComponentsInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        foreach (Animator a in allChildrenAnimators) {
            a.speed = globalGameDate.allLayers[globalGameDate.GetCurrentLayerIndex()].layerSpeedFactors[0];
        }
    }
}
