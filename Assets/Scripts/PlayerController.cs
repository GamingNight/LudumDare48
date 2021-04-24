using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float Speed=10f;

	private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
    	spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
