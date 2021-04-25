using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public GameObject nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveNextLevel()
    {
    	gameObject.SetActive(false);
    	nextLevel.SetActive(true);
        foreach (PlayerController player in gameObject.GetComponentsInChildren<PlayerController>()) {
            player.ResetPosition();
        }
        foreach (ProjectileSpawner spawner in gameObject.GetComponentsInChildren<ProjectileSpawner>()) {
            spawner.Restart();
        }
        foreach (ProjectileMove projectile in gameObject.GetComponentsInChildren<ProjectileMove>()) {
            projectile.gameObject.SetActive(false);
        }
    }
}
