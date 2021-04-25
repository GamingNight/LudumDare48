using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGenerator : MonoBehaviour
{
    public GameObject projectileSpawnerPrefab;
    public float spawnDelayInSecond = 0f;
    public float InstantiateDelayInSecond = 0f;
    public float spawnerFrequency = 0.2f;
    public Vector3 spawnerShift = new Vector3(0f, 0f, 0f);
    public int n = 1;
    public LayerData layerData;
    public GlobalGameData globalGameData;

    private float timeSinceStart;
    private bool instantiated;
    private Vector3 firstSpawnerAtOrigin = new Vector3(0.318f, 0.318f, 0);
    // Start is called before the first frame update
    void Start() {
        Init();
    }

    public void Init() {
        timeSinceStart = 0;
        instantiated = false;
    }

    // Update is called once per frame
    void Update() {
        int currentLayerIndex = globalGameData.GetCurrentLayerIndex();
        int projectileLayerIndex = layerData.layerIndex;
        float speedFactor = globalGameData.allLayers[currentLayerIndex].layerSpeedFactors[projectileLayerIndex];
        timeSinceStart += (Time.deltaTime * speedFactor);
        if (timeSinceStart < InstantiateDelayInSecond || instantiated == true)
            return;
        else {
            InstantiateSpawner();
            instantiated = true;
        }
    }
    private void InstantiateSpawner() {
        for (int i = 0; i < n; i++) {
            GameObject spawner = Instantiate(projectileSpawnerPrefab, transform.position, transform.rotation, transform);
            spawner.transform.localPosition = new Vector3(spawner.transform.localPosition.x, spawner.transform.localPosition.y, spawner.transform.localPosition.z) + i * spawnerShift + firstSpawnerAtOrigin;
            ProjectileSpawner projectileSpawner = spawner.GetComponent<ProjectileSpawner>();
            projectileSpawner.spawnOffsetInSeconds = i * spawnDelayInSecond;
            projectileSpawner.spawnFrequency = spawnerFrequency;
            projectileSpawner.SetFromGenerator(true);
        }
    }
}
