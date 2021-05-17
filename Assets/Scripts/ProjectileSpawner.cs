using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{

    private static readonly float DEFAULT_PROJECTILE_X_SHIFT = 0.2f;
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
            InstantiateProjectile(DEFAULT_PROJECTILE_X_SHIFT);
            timeSinceLastSpawn = 0;
        }
    }

    private GameObject InstantiateProjectile(float xShift) {

        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation, transform);
        projectile.name = "Projectile " + projectiles.Count + " " + gameObject.name;
        projectile.transform.localPosition = new Vector3(projectile.transform.localPosition.x + xShift, projectile.transform.localPosition.y, projectile.transform.localPosition.z);
        float spawnerAngle = transform.eulerAngles.z;
        projectile.GetComponent<ProjectileMove>().direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * spawnerAngle), Mathf.Sin(Mathf.Deg2Rad * spawnerAngle));
        if (spawnerData.initForceFactor != 0) {
            projectile.GetComponent<ProjectileMove>().customData.initForceFactor = spawnerData.initForceFactor;
        }
        projectiles.Add(projectile);
        CleanProjectileList();

        return projectile;
    }

    public void Init() {
        DestroyAllProjectiles();
        timeSinceStart = 0;
        timeSinceLastSpawn = 1f / spawnerData.frequency; //next spawn occurs immediatly after the offset
        if (spawnerData.populate) {
            timeSinceStart = spawnerData.offsetInSeconds; //no more offset, as the spawner has already started (it will be taken into account in the Populate méthod)
            timeSinceLastSpawn = Populate();
        }
    }

    /// <summary>
    /// Populate the spawner by instantiating projectiles. Given the offsetInSeconds, return the adjusted time since last spawn
    /// </summary>
    /// <returns></returns>
    private float Populate() {

        float newTimeSinceLastSpawn = timeSinceLastSpawn;
        if (spawnerData.frequency != 0) {
            bool reachBoundary = false;
            int i = 0;
            float force = spawnerData.initForceFactor != 0 ? spawnerData.initForceFactor : projectilePrefab.GetComponent<ProjectileMove>().defaultData.initForceFactor;
            float velocity = (force / projectilePrefab.GetComponent<Rigidbody2D>().mass) * Time.fixedDeltaTime;
            float distanceBetweenProjectiles = velocity / spawnerData.frequency;
            float firstProjectileXShift = 0;
            bool updateFirstProjectileXShift = true;
            while (!reachBoundary) {
                float xShift = DEFAULT_PROJECTILE_X_SHIFT //add the default shift of the spawner (static constant value)
                        + (i * distanceBetweenProjectiles) //add the distance given the index of the projectile in the line
                        - (velocity * spawnerData.offsetInSeconds); //remove the distance given by the offset of the spawner
                GameObject projectile = InstantiateProjectile(xShift);
                if (updateFirstProjectileXShift) { //Register the shift of the first projectile
                    firstProjectileXShift = xShift;
                    updateFirstProjectileXShift = false;
                }
                bool isBelowSpawnPoint = projectile.transform.localPosition.x - DEFAULT_PROJECTILE_X_SHIFT < -0.001f;
                if (isBelowSpawnPoint) { //Projectile is below the spawner because of the offset
                    //Destroy the projectile
                    Destroy(projectile);
                    //Next projectile becomes the new first projectile
                    updateFirstProjectileXShift = true;
                } else {
                    //Stop populating as soon as the projectile has reached a level boundary
                    Vector2 projectilePosition = projectile.transform.position;
                    if (projectilePosition.x < globalGameData.upLeftBoundary.x || projectilePosition.x > globalGameData.downRightBoundary.x ||
                        projectilePosition.y > globalGameData.upLeftBoundary.y || projectilePosition.y < globalGameData.downRightBoundary.y) {
                        reachBoundary = true;
                    }
                }
                i++;
            }
            //Compute the new time since last spawn (given the position of the first populated projectile)
            newTimeSinceLastSpawn = (firstProjectileXShift - DEFAULT_PROJECTILE_X_SHIFT) / velocity;
        }
        return newTimeSinceLastSpawn;
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
