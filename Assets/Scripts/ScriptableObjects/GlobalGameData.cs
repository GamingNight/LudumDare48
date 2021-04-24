using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Global Game Data")]
public class GlobalGameData : ScriptableObject
{
    public LayerData[] allLayers;
    private int currentLayerIndex = 0;

    public void InitLayer() {
        currentLayerIndex = 0;
    }

    public void IncLayer() {
        currentLayerIndex = (currentLayerIndex + 1) % allLayers.Length;
    }

    public int GetCurrentLayerIndex() {
        return currentLayerIndex;
    }
}
