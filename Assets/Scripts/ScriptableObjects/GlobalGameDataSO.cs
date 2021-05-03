using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Global Game Data")]
public class GlobalGameDataSO : ScriptableObject
{
    public LayerDataSO[] allLayers;
    public PlayerInputSO horizontalInput;
    public PlayerInputSO verticalInput;
    public PlayerInputSO incLayerInput;
    public PlayerInputSO decLayerInput;
    public PlayerInputSO restartInput;

    private int currentLayerIndex = 0;

    public void InitLayer() {
        currentLayerIndex = 0;
    }

    public void IncLayer() {
        int newIndex = currentLayerIndex + 1;
        currentLayerIndex = newIndex < allLayers.Length ? newIndex : currentLayerIndex;
    }

    public void DecLayer() {
        int newIndex = currentLayerIndex - 1;
        currentLayerIndex = newIndex >= 0 ? newIndex : currentLayerIndex;
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

    public bool GetRestartButtonDown() {

        return restartInput.GetButtonDown();
    }

    public void LockInputs() {
        horizontalInput.lockInput = true;
        verticalInput.lockInput = true;
        incLayerInput.lockInput = true;
        decLayerInput.lockInput = true;
        restartInput.lockInput = true;
    }

    public void UnlockInputs() {
        horizontalInput.lockInput = false;
        verticalInput.lockInput = false;
        incLayerInput.lockInput = false;
        decLayerInput.lockInput = false;
        restartInput.lockInput = false;
    }
}
