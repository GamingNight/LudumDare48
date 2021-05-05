using UnityEngine;
using System;

[System.Serializable]
public class SpawnerData
{
    public Vector2 position;
    public float rotation;
    public float frequency;
    public float offsetInSeconds;
    /// <summary>
    /// The init force factor of the projectile. Leave it to 0 if you want to use the default force factor.
    /// </summary>
    public float initForceFactor;

    public SpawnerData Copy() {

        SpawnerData clone = new SpawnerData();
        clone.position = position;
        clone.rotation = rotation;
        clone.frequency = frequency;
        clone.offsetInSeconds = offsetInSeconds;
        clone.initForceFactor = initForceFactor;
        return clone;
    }

    public bool Equals(SpawnerData other) {
        bool equal = true;
        equal &= other.position == position;
        equal &= other.rotation == rotation;
        equal &= other.frequency == frequency;
        equal &= other.offsetInSeconds == offsetInSeconds;
        equal &= other.initForceFactor == initForceFactor;
        return equal;
    }

    public override string ToString() {
        string s = "SpawnerData[";
        s += "(position=" + position.ToString() + "), ";
        s += "(rotation=" + rotation + "), ";
        s += "(frequency=" + frequency + "), ";
        s += "(offset=" + offsetInSeconds + "), ";
        s += "(initForceFactor =" + initForceFactor + ")]";
        return s;
    }
}
