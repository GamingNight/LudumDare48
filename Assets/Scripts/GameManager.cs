using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GlobalGameDataSO globalGameData;
    void Awake() {
        globalGameData.UnlockInputs();
        Cursor.visible = false;
    }
}
