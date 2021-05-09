using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGenerator : MonoBehaviour
{
    public GameObject projectileSpawnerPrefab;
    public WaveData waveData;
    public LayerDataSO layerData;
    public GlobalGameDataSO globalGameData;

    private bool instantiated = false;
    private Vector3 firstSpawnerAtOrigin = new Vector3(0.318f, 0.318f, 0);

    private List<ProjectileSpawner> spawners;
    private WaveData previousData;

    // Start is called before the first frame update
    void Start() {
        spawners = new List<ProjectileSpawner>();
        previousData = waveData.Copy();
        Init();
    }

    public void Init() {
        DestroyAllSpawners();
        instantiated = false;
    }

    // Update is called once per frame
    void Update() {
        CheckForLevelEditing();
        transform.position = waveData.position;
        transform.eulerAngles = new Vector3(0, 0, waveData.rotation);
        if (!instantiated) {
            InstantiateSpawners();
            instantiated = true;
        }
    }
    private void InstantiateSpawners() {
        for (int i = 0; i < waveData.nbSpawner; i++) {
            GameObject spawner = Instantiate(projectileSpawnerPrefab, transform.position, transform.rotation, transform);
            spawner.name = "Spawner " + spawners.Count + " " + gameObject.name;
            spawner.transform.localPosition = new Vector3(spawner.transform.localPosition.x, spawner.transform.localPosition.y, spawner.transform.localPosition.z) + i * waveData.spawnerShift + firstSpawnerAtOrigin;
            ProjectileSpawner projectileSpawner = spawner.GetComponent<ProjectileSpawner>();
            projectileSpawner.spawnerData.offsetInSeconds = waveData.offsetInSeconds + (i * waveData.spawnerOffsetInSeconds);
            projectileSpawner.spawnerData.frequency = waveData.spawnerFrequency;
            projectileSpawner.SetFromGenerator(true);
            if (waveData.initForceFactor != 0) {
                projectileSpawner.spawnerData.initForceFactor = waveData.initForceFactor;
            }
            projectileSpawner.spawnerData.populate = waveData.populate;
            spawners.Add(projectileSpawner);
        }
    }

    public void DestroyAllSpawners() {

        foreach (ProjectileSpawner spawner in spawners) {
            spawner.DestroyAllProjectiles();
            Destroy(spawner.gameObject);
        }
        spawners.Clear();
    }
    private void CheckForLevelEditing() {
        //If the wave has been updated in the level scriptable object, reinit the wave
        if (!waveData.Equals(previousData)) {
            previousData = waveData.Copy();
            Init();
        } else { //if the wave has been updated in the scene, update the level scriptable object
            if ((Vector2)transform.position != waveData.position) {
                waveData.position = transform.position;
                previousData = waveData.Copy();
            }
            if (transform.eulerAngles.z != waveData.rotation) {
                waveData.rotation = transform.eulerAngles.z;
                previousData = waveData.Copy();
            }
        }
    }
}
