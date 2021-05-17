using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitActionSO : AbstractSwitchTileActionSO
{
    public override void Trigger() {
        Application.Quit();
    }

    public override ActionType GetActionType() {

        return ActionType.QUIT;
    }
}
