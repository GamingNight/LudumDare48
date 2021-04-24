using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Global Game Data")]
public class GlobalGameData : ScriptableObject
{
    public LayerData[] allLayers;
    private int currentLayerIndex = 0;
    private int maxIndex = 2;

    public void InitLayer() {
        currentLayerIndex = 0;
    }

    public void IncLayer(bool force = false) {
        int newIndex = (currentLayerIndex + 1);
        if (!force && (newIndex >  maxIndex))
        {
            return;
        }
        currentLayerIndex = newIndex % allLayers.Length;
    }

    public void DecLayer(bool force = false) {
        int newIndex = (currentLayerIndex - 1);
        if (!force && (newIndex <  0))
        {
            return;
        }
        currentLayerIndex = newIndex;
        if (currentLayerIndex == -1)
            currentLayerIndex = allLayers.Length - 1;
    }

    public int GetCurrentLayerIndex() {
        return currentLayerIndex;
    }
}
