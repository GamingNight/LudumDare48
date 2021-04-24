using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{

    public bool active;
    public GameObject projectilePrefab;
    public float spawnFrequency;
    public bool spawnAtStart;

    private float timeSinceLastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAtStart) {
            InstantiateProjectile();
        }
        timeSinceLastSpawn += Time.deltaTime;

        //if(timeSinceLastSpawn > )

    }

    private void InstantiateProjectile() {

        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }
}
