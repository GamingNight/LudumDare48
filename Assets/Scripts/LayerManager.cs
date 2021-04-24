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
        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)) {
            globalGameData.IncLayer(true);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            globalGameData.IncLayer();
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            globalGameData.DecLayer();
        }
    }
}
