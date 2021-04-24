using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float Speed=10f;

	private string lastDir = "Right";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (lastDir != "right")
            {
            	Back2Normal();
            	lastDir = "right";
            }
            transform.Translate(Vector3.right*Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (lastDir != "left")
            {
            	Back2Normal();
            	transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
            	lastDir = "left";
            }
            transform.Translate(Vector3.right*Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {  
            if (lastDir != "up")
            {
            	Back2Normal();
            	transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            	lastDir = "up";
            }
            transform.Translate(Vector3.right*Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (lastDir != "down")
            {
            	Back2Normal();
            	transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
            	lastDir = "down";
            }
            transform.Translate(Vector3.right*Time.deltaTime*Speed);
        }
    }

    void Back2Normal()
    {
    	if (lastDir == "left")
    	{
    		transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
    	}
    	if (lastDir == "up")
    	{
    		transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
    	}
    	if (lastDir == "down")
    	{
    		transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
    	}
    	lastDir = "Right";
    }
}
