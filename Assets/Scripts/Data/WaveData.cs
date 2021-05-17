using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public Vector2 position;
    public float rotation;
    public float offsetInSeconds;
    public int nbSpawner;
    public float spawnerOffsetInSeconds;
    public bool reverseOffsetOrder;
    public float spawnerFrequency;
    public Vector3 spawnerShift;
    /// <summary>
    /// The init force factor of the projectile. Leave it to 0 if you want to use the default force factor.
    /// </summary>
    public float initForceFactor;
    public bool populate;

    public WaveData Copy() {

        WaveData clone = new WaveData();
        clone.position = position;
        clone.rotation = rotation;
        clone.offsetInSeconds = offsetInSeconds;
        clone.nbSpawner = nbSpawner;
        clone.spawnerOffsetInSeconds = spawnerOffsetInSeconds;
        clone.reverseOffsetOrder = reverseOffsetOrder;
        clone.spawnerFrequency = spawnerFrequency;
        clone.spawnerShift = spawnerShift;
        clone.initForceFactor = initForceFactor;
        clone.populate = populate;
        return clone;
    }

    public bool Equals(WaveData other) {
        bool equal = true;
        equal &= other.position == position;
        equal &= other.rotation == rotation;
        equal &= other.spawnerOffsetInSeconds == spawnerOffsetInSeconds;
        equal &= other.offsetInSeconds == offsetInSeconds;
        equal &= other.reverseOffsetOrder == reverseOffsetOrder;
        equal &= other.spawnerFrequency == spawnerFrequency;
        equal &= other.spawnerShift == spawnerShift;
        equal &= other.nbSpawner == nbSpawner;
        equal &= other.initForceFactor == initForceFactor;
        equal &= other.populate == populate;
        return equal;
    }
}
