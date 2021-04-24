using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherController : MonoBehaviour
{
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
        		Debug.Log("You Win !! GG boy");
        	}
            
        }
	}
}
