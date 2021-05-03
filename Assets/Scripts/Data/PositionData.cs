using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionData
{
    public Vector2 position;

    public PositionData Copy() {

        PositionData clone = new PositionData();
        clone.position = position;
        return clone;
    }
}
