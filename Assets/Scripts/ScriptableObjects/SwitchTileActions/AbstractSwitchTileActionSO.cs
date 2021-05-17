using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSwitchTileActionSO : ScriptableObject
{
    public enum ActionType
    {
        LEVEL_SELECTOR, QUIT
    }

    public abstract void Trigger();

    public abstract ActionType GetActionType();
}
