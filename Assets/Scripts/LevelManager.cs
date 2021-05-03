using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GlobalGameDataSO globalGameData;
    public LevelDataSO[] levelDataSOList;
    public PlayerController playerController;
    public SpriteRendererController backgroundSpriteRenderer;
    public SpriteRendererController levelNameSpriteRenderer;
    public GameObject switchTilePrefab;
    public GameObject layer0SpawnerPrefab;
    public GameObject layer1SpawnerPrefab;
    public GameObject layer2SpawnerPrefab;
    public GameObject layer0WavePrefab;
    public GameObject layer1WavePrefab;
    public GameObject layer2WavePrefab;
    public GameObject endFireworkPrefab;

    public int defaultLevelIndex = 0;

    private int currentLevelIndex;

    private List<GameObject> currentLevelTileList;
    private List<ProjectileSpawner> currentLevelSpawnerList;
    private List<SpawnerGenerator> currentLevelWaveList;
    private GameObject endFireWork;

    void Start() {
        currentLevelIndex = defaultLevelIndex;
        currentLevelTileList = new List<GameObject>();
        currentLevelSpawnerList = new List<ProjectileSpawner>();
        currentLevelWaveList = new List<SpawnerGenerator>();

        LoadLevel(currentLevelIndex);
    }

    private void Update() {
        if (globalGameData.GetRestartButtonDown()) {
            ResetCurrentLevel();
        }
    }

    public void GoToLevel(int index) {
        UpdateLevel(index);
        currentLevelIndex = index;
    }

    private void UpdateLevel(int newIndex) {
        ReleaseCurrentLevel();
        LoadLevel(newIndex);
    }

    private void LoadLevel(int index) {

        LevelDataSO level = FindLevel(index);

        //Set player position
        playerController.initPositionData = level.playerSpawnPosition;
        playerController.ResetPosition();

        //Instantiate switch tiles
        currentLevelTileList.Clear();
        InstantiateSwitchTiles(level);

        //Set graphical stuff (background & level name sprites)
        if (level.backgroundSpriteData != null) {
            backgroundSpriteRenderer.spriteData = level.backgroundSpriteData;
            backgroundSpriteRenderer.Init();
        }
        if (level.levelIndicatorSpriteData != null) {
            levelNameSpriteRenderer.spriteData = level.levelIndicatorSpriteData;
            levelNameSpriteRenderer.Init();
        }

        //Instantiate spawners
        currentLevelSpawnerList.Clear();
        InstantiateSpawners(level);


        //Instantiate waves (aka spawner generator)
        currentLevelWaveList.Clear();
        InstantiateWaves(level);

        //End level firework
        if (level.isEndLevel) {
            endFireWork = Instantiate(endFireworkPrefab);
            endFireWork.transform.parent = transform;
            endFireWork.name = "Firework";
        }
    }

    public void InstantiateSwitchTiles(LevelDataSO level) {
        foreach (SwitchTileData switchTileData in level.switchTileDataList) {
            InstantiateSwitchTile(switchTileData);
        }
    }

    public void InstantiateSwitchTile(SwitchTileData switchTileData) {
        GameObject switchTileClone = Instantiate(switchTilePrefab, switchTileData.position, Quaternion.identity);
        switchTileClone.transform.parent = transform;
        switchTileClone.name = ComputeTileName(switchTileClone, switchTileData);
        SwitchTileController switchTileScript = switchTileClone.GetComponent<SwitchTileController>();
        switchTileScript.levelManager = this;
        switchTileScript.tileData = switchTileData;
        currentLevelTileList.Add(switchTileClone);
    }
    private string ComputeTileName(GameObject tile, SwitchTileData data) {

        string name = "Tile " + (currentLevelTileList.Count + 1) + " " + data.actionType.ToString();
        switch (data.actionType) {
            case AbstractSwitchTileAction.ActionType.QUIT:
                break;
            case AbstractSwitchTileAction.ActionType.LEVEL_SELECTOR:
                name += " " + data.targetLevelIndex;
                break;
            default:
                break;
        }
        return name;
    }

    public void InstantiateSpawners(LevelDataSO level) {
        //Layer 0
        InstantiateLayerSpanwers(level.layer0SpawnerDataList, layer0SpawnerPrefab);
        //Layer 1
        InstantiateLayerSpanwers(level.layer1SpawnerDataList, layer1SpawnerPrefab);
        //Layer 2
        if (layer2SpawnerPrefab != null) {
            InstantiateLayerSpanwers(level.layer2SpawnerDataList, layer2SpawnerPrefab);
        }
    }

    public void InstantiateLayerSpanwers(SpawnerData[] layerSpawnerDataList, GameObject spawnerPrefab) {
        foreach (SpawnerData spawnerData in layerSpawnerDataList) {
            InstantiateSpawner(spawnerData, spawnerPrefab);
        }
    }

    public void InstantiateSpawner(SpawnerData spawnerData, GameObject spawnerPrefab) {
        GameObject spawner = Instantiate(spawnerPrefab, spawnerData.position, Quaternion.Euler(0, 0, spawnerData.rotation));
        spawner.transform.parent = transform;
        ProjectileSpawner spawnerScript = spawner.GetComponent<ProjectileSpawner>();
        spawner.name = "Spawner " + (currentLevelSpawnerList.Count + 1) + " layer " + spawnerScript.layerData.layerIndex;
        spawnerScript.spawnerData = spawnerData;
        currentLevelSpawnerList.Add(spawnerScript);
    }

    public void InstantiateWaves(LevelDataSO level) {
        //Layer 0
        IntantiateLayerWave(level.layer0WaveDataList, layer0WavePrefab);
        //Layer 1
        IntantiateLayerWave(level.layer1WaveDataList, layer1WavePrefab);
        //Layer 2
        if (layer2WavePrefab != null) {
            IntantiateLayerWave(level.layer2WaveDataList, layer2WavePrefab);
        }
    }

    public void IntantiateLayerWave(WaveData[] layerWaveDataList, GameObject wavePrefab) {
        foreach (WaveData waveData in layerWaveDataList) {
            InstantiateWave(waveData, wavePrefab);
        }
    }

    public void InstantiateWave(WaveData waveData, GameObject wavePrefab) {
        GameObject wave = Instantiate(wavePrefab, waveData.position, Quaternion.Euler(0, 0, waveData.rotation));
        wave.transform.parent = transform;
        SpawnerGenerator waveScript = wave.GetComponent<SpawnerGenerator>();
        wave.name = "Wave " + (currentLevelWaveList.Count + 1) + " layer " + waveScript.layerData.layerIndex;
        waveScript.waveData = waveData;
        currentLevelWaveList.Add(waveScript);
    }

    /// <summary>
    /// Destroy everything from the currently loaded level
    /// </summary>
    private void ReleaseCurrentLevel() {

        playerController.transform.position = Vector2.zero;
        DestroySwitchTiles();
        DestroySpawners();
        DestroyWaves();
        if (endFireWork != null)
            Destroy(endFireWork);
    }

    /// <summary>
    /// Destroy all currently instantiated switch tiles (i.e., tiles from the currently loaded level)
    /// </summary>
    public void DestroySwitchTiles() {
        foreach (GameObject tile in currentLevelTileList) {
            Destroy(tile);
        }
        currentLevelTileList.Clear();
    }

    /// <summary>
    /// Destroy all currently instantiated spawners (i.e., spawners from the currently loaded level)
    /// </summary>
    public void DestroySpawners() {
        foreach (ProjectileSpawner spawner in currentLevelSpawnerList) {
            spawner.DestroyAllProjectiles();
            Destroy(spawner.gameObject);
        }
        currentLevelSpawnerList.Clear();
    }

    /// <summary>
    /// Destroy all currently instantiated waves (i.e., waves from the currently loaded level)
    /// </summary>
    public void DestroyWaves() {
        foreach (SpawnerGenerator wave in currentLevelWaveList) {
            wave.DestroyAllSpawners();
            Destroy(wave.gameObject);
        }
        currentLevelWaveList.Clear();
    }

    /// <summary>
    /// Reset everything from the currently loaded level
    /// </summary>
    public void ResetCurrentLevel() {

        playerController.ResetPosition();
        foreach (ProjectileSpawner spawner in currentLevelSpawnerList) {
            spawner.Init();
        }
        foreach (SpawnerGenerator wave in currentLevelWaveList) {
            wave.Init();
        }
        globalGameData.InitLayer();
    }

    private LevelDataSO FindLevel(int levelIndex) {

        LevelDataSO res = null;
        int i = 0;
        bool found = false;
        while (!found && i < levelDataSOList.Length) {
            if (levelDataSOList[i].levelIndex == levelIndex) {
                found = true;
                res = levelDataSOList[i];
            }
            i++;
        }

        if (res == null) {
            Debug.LogError("Error: Could not find level " + levelIndex);
        }
        return res;
    }

    public ProjectileSpawner FindSpawner(SpawnerData data) {
        ProjectileSpawner spawner = null;
        int i = 0;
        bool found = false;
        while (!found && i < currentLevelSpawnerList.Count) {
            if (currentLevelSpawnerList[i].spawnerData.Equals(data)) {
                spawner = currentLevelSpawnerList[i];
                found = true;
            }
            i++;
        }

        return spawner;
    }
}
