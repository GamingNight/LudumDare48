using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{

    public bool active = true;
    public GameObject projectilePrefab;
    public SpawnerData spawnerData;
    public LayerDataSO layerData;
    public GlobalGameDataSO globalGameData;

    private bool fromGenerator = false;

    private float timeSinceLastSpawn;
    private float timeSinceStart;

    private List<GameObject> projectiles;
    private SpawnerData previousData;

    // Start is called before the first frame update
    void Start() {
        projectiles = new List<GameObject>();
        previousData = spawnerData.Copy();
        Init();
    }

    // Update is called once per frame
    void Update() {
        CheckForLevelEditing();
        transform.position = spawnerData.position;
        transform.eulerAngles = new Vector3(0, 0, spawnerData.rotation);
        int currentLayerIndex = globalGameData.GetCurrentLayerIndex();
        int projectileLayerIndex = layerData.layerIndex;
        float speedFactor = 1;
        if (currentLayerIndex >= projectileLayerIndex)
            speedFactor = globalGameData.allLayers[currentLayerIndex].layerSpeedFactors[projectileLayerIndex];
        timeSinceStart += (Time.deltaTime * speedFactor);
        if (timeSinceStart < spawnerData.offsetInSeconds)
            return;
        timeSinceLastSpawn += (Time.deltaTime * speedFactor);
        if (timeSinceLastSpawn >= 1f / spawnerData.frequency) {
            InstantiateProjectile();
            timeSinceLastSpawn = 0;
        }
    }

    private void InstantiateProjectile() {

        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation, transform);
        projectile.name = "Projectile " + projectiles.Count + " " + gameObject.name;
        projectile.transform.localPosition = new Vector3(projectile.transform.localPosition.x + 0.2f, projectile.transform.localPosition.y, projectile.transform.localPosition.z);
        if (transform.eulerAngles.z == 0) {
            projectile.GetComponent<ProjectileMove>().direction = ProjectileMove.Direction.RIGHT;
        } else if (transform.eulerAngles.z == 90) {
            projectile.GetComponent<ProjectileMove>().direction = ProjectileMove.Direction.UP;
        } else if (transform.eulerAngles.z == 180) {
            projectile.GetComponent<ProjectileMove>().direction = ProjectileMove.Direction.LEFT;
        } else if (transform.eulerAngles.z == 270) {
            projectile.GetComponent<ProjectileMove>().direction = ProjectileMove.Direction.DOWN;
        }
        if (spawnerData.initForceFactor != 0) {
            projectile.GetComponent<ProjectileMove>().customData.initForceFactor = spawnerData.initForceFactor;
        }
        projectiles.Add(projectile);
        CleanProjectileList();
    }

    public void Init() {
        DestroyAllProjectiles();
        timeSinceStart = 0;
        timeSinceLastSpawn = 1f / spawnerData.frequency; //in order to spawn immediatly after the offset
    }

    public void DestroyAllProjectiles() {

        CleanProjectileList();
        foreach (GameObject p in projectiles) {
            Destroy(p);
        }
        projectiles.Clear();
    }

    /// <summary>
    /// Remove projectiles that have already been destroyed (collision with a wall, or with another projectile)
    /// </summary>
    private void CleanProjectileList() {

        List<GameObject> _projectiles = new List<GameObject>(projectiles);
        foreach (GameObject p in _projectiles) {
            if (p == null)
                projectiles.Remove(p);
        }
    }

    public bool IsFromGenerator() {
        return fromGenerator;
    }

    public void SetFromGenerator(bool b) {
        fromGenerator = b;
    }

    private void CheckForLevelEditing() {
        //If the spawner has been updated in the level scriptable object, reinit the spawner
        if (!spawnerData.Equals(previousData)) {
            previousData = spawnerData.Copy();
            Init();
        } else { //if the spawner has been updated in the scene, update the level scriptable object
            if ((Vector2)transform.position != spawnerData.position) {
                spawnerData.position = transform.position;
                previousData = spawnerData.Copy();
            }
            if (transform.eulerAngles.z != spawnerData.rotation) {
                spawnerData.rotation = transform.eulerAngles.z;
                previousData = spawnerData.Copy();
            }
        }
    }
}
