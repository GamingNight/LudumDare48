using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Level")]
public class LevelDataSO : ScriptableObject
{
    public int levelIndex;
    public PositionData playerSpawnPosition;
    public SwitchTileData[] switchTileDataList;
    public SpriteData levelIndicatorSpriteData;
    public SpriteData backgroundSpriteData;

    public SpawnerData[] layer0SpawnerDataList;
    public SpawnerData[] layer1SpawnerDataList;
    public SpawnerData[] layer2SpawnerDataList;
    public WaveData[] layer0WaveDataList;
    public WaveData[] layer1WaveDataList;
    public WaveData[] layer2WaveDataList;

    public bool isEndLevel;
}
