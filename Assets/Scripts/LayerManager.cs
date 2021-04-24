using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public GlobalGameData globalGameData;

    private void Awake() {
        globalGameData.InitLayer();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            globalGameData.IncLayer();
        }
    }
}