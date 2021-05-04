using System;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SwitchTileData
{
    public Vector2 position;
    public AbstractSwitchTileAction.ActionType actionType;
    public Color color;
    public int targetLevelIndex;
    public Sprite textSprite;
    public Sprite[] levelDigitSprites;

    public SwitchTileData Copy() {

        SwitchTileData clone = new SwitchTileData();
        clone.position = position;
        clone.actionType = actionType;
        clone.color = color;
        clone.targetLevelIndex = targetLevelIndex;
        clone.textSprite = textSprite;
        clone.levelDigitSprites = new Sprite[levelDigitSprites.Length];
        Array.Copy(levelDigitSprites, clone.levelDigitSprites, levelDigitSprites.Length);

        return clone;
    }

    public bool Equals(SwitchTileData other) {

        bool equals = true;
        equals &= other.position == position;
        equals &= other.actionType == actionType;
        equals &= other.color == color;
        equals &= other.targetLevelIndex == targetLevelIndex;
        equals &= other.textSprite == textSprite;
        if (levelDigitSprites == null)
            equals &= other.levelDigitSprites == null;
        else {
            equals &= other.levelDigitSprites.SequenceEqual(levelDigitSprites);
        }
        return equals;
    }
}
