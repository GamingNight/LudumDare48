using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public GameObject[] levels;
    public GameObject selectionLevelLevel;
	public GlobalGameData globalGameData;
	private GameObject currentLevel;
	private int currentIndex;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        currentIndex = 0;
        currentLevel = levels[currentIndex];
        currentLevel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveNextLevel(int index = -1)
    {
    	currentLevel.gameObject.SetActive(false);
        resetLevel(currentLevel);

        if (index < -1)
        {
            SpecialAction(index);
            return;
        }

        if (index != -1)
        {
            currentIndex = index;
        }
        else
        {
            currentIndex = currentIndex + 1;
        }

        if (currentIndex >= levels.Length)
        {
            currentIndex = 0;
        }	

    	currentLevel = levels[currentIndex];
        currentLevel.SetActive(true);
    }

    public void resetLevel(GameObject level)
    {
        coroutine = WaitUnlockCommand(10.0f);
        StartCoroutine(coroutine);
        foreach (PlayerController player in level.GetComponentsInChildren<PlayerController>()) {
            player.ResetPosition();
        }
        foreach (ProjectileSpawner spawner in level.GetComponentsInChildren<ProjectileSpawner>()) {
            if (spawner.IsFromGenerator())
            {
                Destroy(spawner.gameObject);
            } else {
                spawner.Init();
            }
        }
        foreach (SpawnerGenerator spawnerGenerator in level.GetComponentsInChildren<SpawnerGenerator>()) {
            spawnerGenerator.Init();
        }
        foreach (ProjectileMove projectile in level.GetComponentsInChildren<ProjectileMove>()) {
        	Destroy(projectile.gameObject);
        	//projectile.gameObject.SetActive(false);
        }
        globalGameData.InitLayer();
    }
    private IEnumerator WaitUnlockCommand(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            globalGameData.UnlockInputs();
        }
    }

    public void resetLevel()
    {
    	resetLevel(gameObject);
    }

    private void SpecialAction(int index)
    {
        if (index == -100)
        {
            if (selectionLevelLevel == null)
            {
                ActiveNextLevel(0);
                return;
            }
            currentLevel = selectionLevelLevel;
            currentLevel.SetActive(true);
        }
        if (index == -200)
        {
            Application.Quit();
        }
    }
}
