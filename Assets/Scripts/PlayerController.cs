using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float Speed=10f;
	public LayerData layerData;
	public GlobalGameData globalGameData;

	private SpriteRenderer spriteRenderer;
	private int previousLayerIndex;

    // Start is called before the first frame update
    void Start()
    {
    	spriteRenderer = GetComponent<SpriteRenderer>();
    	previousLayerIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
    	int currentLayerIndex = globalGameData.GetCurrentLayerIndex();
    	if (currentLayerIndex != previousLayerIndex) {
    		if (previousLayerIndex > currentLayerIndex)
    		{
    			// create new dummy player
    			// save pos for the respown

    		} else {
    			// delete child dummy player
    			// translate saved pos
    		}
    		previousLayerIndex = currentLayerIndex;
    	}
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right*Time.deltaTime*Speed);
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left*Time.deltaTime*Speed);
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {  
            transform.Translate(new Vector3(0,1,0)*Time.deltaTime*Speed);
            spriteRenderer.flipY = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0,-1,0)*Time.deltaTime*Speed);
            spriteRenderer.flipY = false;
        }
    }

}
