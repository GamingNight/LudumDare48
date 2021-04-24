using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{

    public bool active = true;
    public GameObject projectilePrefab;
    public float spawnFrequency;
    public float spawnOffsetInSeconds;
    public LayerData layerData;
    public GlobalGameData globalGameData;

    private float timeSinceLastSpawn;
    private float timeSinceStart;

    // Start is called before the first frame update
    void Start() {
        timeSinceStart = 0;
        timeSinceLastSpawn = 1f / spawnFrequency; //in order to spawn immediatly after the offset
    }

    // Update is called once per frame
    void Update() {

        int currentLayerIndex = globalGameData.GetCurrentLayerIndex();
        int projectileLayerIndex = layerData.layerIndex;
        float speedFactor = globalGameData.allLayers[currentLayerIndex].layerSpeedFactors[projectileLayerIndex];
        
        timeSinceStart += (Time.deltaTime * speedFactor);
        if (timeSinceStart < spawnOffsetInSeconds)
            return;

        timeSinceLastSpawn += (Time.deltaTime * speedFactor);
        if (timeSinceLastSpawn >= 1f / spawnFrequency) {
            InstantiateProjectile();
            timeSinceLastSpawn = 0;
        }

    }

    private void InstantiateProjectile() {

        GameObject clone = Instantiate(projectilePrefab, transform.position, transform.rotation, transform);
        clone.transform.localPosition = new Vector3(clone.transform.localPosition.x + 1f, clone.transform.localPosition.y, clone.transform.localPosition.z);
        if (transform.eulerAngles.z == 0) {
            clone.GetComponent<ProjectileMove>().direction = ProjectileMove.Direction.RIGHT;
        } else if (transform.eulerAngles.z == 90) {
            clone.GetComponent<ProjectileMove>().direction = ProjectileMove.Direction.UP;
        } else if (transform.eulerAngles.z == 180) {
            clone.GetComponent<ProjectileMove>().direction = ProjectileMove.Direction.LEFT;
        } else if (transform.eulerAngles.z == 270) {
            clone.GetComponent<ProjectileMove>().direction = ProjectileMove.Direction.DOWN;
        }
    }
}
