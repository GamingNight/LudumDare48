using UnityEngine;
using System;

[System.Serializable]
public class SpawnerData
{
    public Vector2 position;
    public float rotation;
    public float frequency;
    public float offsetInSeconds;

    public SpawnerData Copy() {

        SpawnerData clone = new SpawnerData();
        clone.position = position;
        clone.rotation = rotation;
        clone.frequency = frequency;
        clone.offsetInSeconds = offsetInSeconds;

        return clone;
    }

    public bool Equals(SpawnerData other) {
        bool equal = true;
        equal &= other.position == position;
        equal &= other.rotation == rotation;
        equal &= other.frequency == frequency;
        equal &= other.offsetInSeconds == offsetInSeconds;
        return equal;
    }

    public override string ToString() {
        string s = "SpawnerData[";
        s += "(position=" + position.ToString() + "), ";
        s += "(rotation=" + rotation + "), ";
        s += "(frequency=" + frequency + "), ";
        s += "(offset=" + offsetInSeconds + ")]";
        return s;
    }
}
