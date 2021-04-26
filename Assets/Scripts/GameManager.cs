using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GlobalGameData globalGameData;
    void Awake() {
        globalGameData.UnlockInputs();
    }
}
