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
    public float spawnerFrequency;
    public Vector3 spawnerShift;

    public WaveData Copy() {

        WaveData clone = new WaveData();
        clone.position = position;
        clone.rotation = rotation;
        clone.offsetInSeconds = offsetInSeconds;
        clone.nbSpawner = nbSpawner;
        clone.spawnerOffsetInSeconds = spawnerOffsetInSeconds;
        clone.spawnerFrequency = spawnerFrequency;
        clone.spawnerShift = spawnerShift;
        return clone;
    }

    public bool Equals(WaveData other) {
        bool equal = true;
        equal &= other.position == position;
        equal &= other.rotation == rotation;
        equal &= other.spawnerOffsetInSeconds == spawnerOffsetInSeconds;
        equal &= other.offsetInSeconds == offsetInSeconds;
        equal &= other.spawnerFrequency == spawnerFrequency;
        equal &= other.spawnerShift == spawnerShift;
        equal &= other.nbSpawner == nbSpawner;
        return equal;
    }
}
