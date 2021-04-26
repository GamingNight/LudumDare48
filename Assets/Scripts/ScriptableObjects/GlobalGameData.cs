using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Global Game Data")]
public class GlobalGameData : ScriptableObject
{
    //public enum PlayerInput
    //{
    //    MOVE_HORIZONTAL, MOVE_VERTICAL, INC_LAYER, DEC_LAYER
    //}

    public LayerData[] allLayers;
    public PlayerInput horizontalInput;
    public PlayerInput verticalInput;
    public PlayerInput incLayerInput;
    public PlayerInput decLayerInput;

    private int currentLayerIndex = 0;
    private int maxIndex = 2;

    public void InitLayer() {
        currentLayerIndex = 0;
    }

    public void IncLayer(bool force = false) {
        int newIndex = (currentLayerIndex + 1);
        if (!force && (newIndex > maxIndex)) {
            return;
        }
        currentLayerIndex = newIndex % allLayers.Length;
    }

    public void DecLayer(bool force = false) {
        int newIndex = (currentLayerIndex - 1);
        if (!force && (newIndex < 0)) {
            return;
        }
        currentLayerIndex = newIndex;
        if (currentLayerIndex == -1)
            currentLayerIndex = allLayers.Length - 1;
    }

    public int GetCurrentLayerIndex() {
        return currentLayerIndex;
    }

    public float GetPlayerHorizontalMove() {

        return horizontalInput.GetAxisRaw();
    }

    public float GetPlayerVerticalMove() {

        return verticalInput.GetAxisRaw();
    }

    public bool GetIncLayerButtonDown() {

        return incLayerInput.GetButtonDown();
    }

    public bool GetDecLayerButtonDown() {

        return decLayerInput.GetButtonDown();
    }
}
