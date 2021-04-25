using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherController : MonoBehaviour
{
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("collision.gameObject.tag = " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player") {
        	int index = collision.gameObject.GetComponent<PlayerController>().GetPreviousLayerIndex();
        	
        	if (index == 0)
        	{
                Transform t = transform;
                int count = 0;
                while (levelManager == null) {
                    t = t.parent;
                    levelManager = t.GetComponent<LevelManager>();
                    count = count + 1;
                    if (count > 5)
                    {
                        Debug.Log("infinit loop (deeper and deeper)");
                        Debug.Log(count);
                        return;
                    }
                    
                }

                levelManager.ActiveNextLevel();

        	}
            
        }
	}
}
