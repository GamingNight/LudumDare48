using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public GameObject[] levels;
	public GlobalGameData globalGameData;
	private GameObject currentLevel;
	private int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        currentLevel = levels[currentIndex];
        currentLevel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveNextLevel()
    {
    	currentLevel.gameObject.SetActive(false);
        resetLevel(currentLevel);
    	currentIndex = currentIndex + 1;
    	if (currentIndex == levels.Length)
    	{
    		currentIndex = 0;
    	}
    	currentLevel = levels[currentIndex];
        currentLevel.SetActive(true);
    }

    public void resetLevel(GameObject level)
    {
    	foreach (PlayerController player in level.GetComponentsInChildren<PlayerController>()) {
            player.ResetPosition();
        }
        foreach (ProjectileSpawner spawner in level.GetComponentsInChildren<ProjectileSpawner>()) {
            spawner.Restart();
        }
        foreach (ProjectileMove projectile in level.GetComponentsInChildren<ProjectileMove>()) {
        	// FIXME Destroy(projectile.gameObject);
        	projectile.gameObject.SetActive(false);
        }
        globalGameData.InitLayer();
    }

    public void resetLevel()
    {
    	resetLevel(gameObject);
    }
}
