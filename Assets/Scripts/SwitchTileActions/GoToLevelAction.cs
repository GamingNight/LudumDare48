using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GoToLevelAction : AbstractSwitchTileAction
{
    public int LevelIndex { get; set; }
    public LevelManager LevelManager { get; set; }

    public override void Trigger() {
        LevelManager.GoToLevel(LevelIndex);
    }

    public override ActionType GetActionType() {

        return ActionType.LEVEL_SELECTOR;
    }
}
