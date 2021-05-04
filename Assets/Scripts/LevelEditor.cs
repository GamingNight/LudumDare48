using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    private LevelManager levelManager;

    private LevelDataSO level;
    private List<SwitchTileData> sceneSwitchTileDataList;
    private List<SpawnerData> sceneSpawnerDataList;
    private List<WaveData> sceneWaveDataList;

    private void Start() {
        levelManager = GetComponent<LevelManager>();
    }

    void Update() {
        if (level == null) {
            level = levelManager.GetCurrentLevelData();
            UpdateSceneSwitchTileList();
            UpdateSceneSpawnerList();
            UpdateSceneWaveList();
        } else {
            level = levelManager.GetCurrentLevelData();
            //If switch tile are added or removed from the level scriptable object, update the scene accordingly
            int levelAllSwitchTileCount = level.switchTileDataList.Length;
            if (levelAllSwitchTileCount != sceneSwitchTileDataList.Count) {
                levelManager.DestroySwitchTiles();
                levelManager.InstantiateSwitchTiles(level);
            }
            UpdateSceneSwitchTileList();

            //If spawners are added or removed from the level scriptable object, update the scene accordingly
            int levelAllSpawnerCount = level.layer0SpawnerDataList.Length + level.layer1SpawnerDataList.Length + level.layer2SpawnerDataList.Length;
            if (levelAllSpawnerCount != sceneSpawnerDataList.Count) {
                levelManager.DestroySpawners();
                levelManager.InstantiateSpawners(level);
            }
            UpdateSceneSpawnerList();

            //If waves are added or removed from the level scriptable object, update the scene accordingly
            int levelAllWaveCount = level.layer0WaveDataList.Length + level.layer1WaveDataList.Length + level.layer2WaveDataList.Length;
            if (levelAllWaveCount != sceneWaveDataList.Count) {
                levelManager.DestroyWaves();
                levelManager.InstantiateWaves(level);
            }
            UpdateSceneWaveList();
        }
    }

    private void UpdateSceneSwitchTileList() {
        sceneSwitchTileDataList = new List<SwitchTileData>(level.switchTileDataList);
    }

    private void UpdateSceneSpawnerList() {
        sceneSpawnerDataList = new List<SpawnerData>(level.layer0SpawnerDataList);
        sceneSpawnerDataList.AddRange(level.layer1SpawnerDataList);
        sceneSpawnerDataList.AddRange(level.layer2SpawnerDataList);
    }

    private void UpdateSceneWaveList() {
        sceneWaveDataList = new List<WaveData>(level.layer0WaveDataList);
        sceneWaveDataList.AddRange(level.layer1WaveDataList);
        sceneWaveDataList.AddRange(level.layer2WaveDataList);
    }
}
