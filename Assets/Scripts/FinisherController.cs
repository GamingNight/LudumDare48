using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherController : MonoBehaviour
{
    public int sceneIndexTarget = -1;
    private LevelManager levelManager;

    public GameObject loader;
    private Animator loaderAnim;

    // Start is called before the first frame update
    void Start()
    {
        loaderAnim = loader.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Player") {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        	int index = player.GetPreviousLayerIndex();
        	
        	if (index == 0)
        	{
                Transform t = transform;
                int count = 0;
                while (levelManager == null) {
                    t = t.parent;
                    levelManager = t.GetComponent<LevelManager>();
                    count = count + 1;
                    if (count > 20)
                    {
                        Debug.Log("infinit loop (deeper and deeper)");
                        return;
                    }
                    
                }
                levelManager.ActiveNextLevel(sceneIndexTarget);

        	}

            //Loader
            loaderAnim.SetBool("Loading", true);

        }
	}
}
