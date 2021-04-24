using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public enum Direction
    {
        UP, DOWN, LEFT, RIGHT, NONE
    }

	public float Speed=10f;
	public TriggerZoneController triggerZone;

	public ProjectileMove.Direction lastDir = ProjectileMove.Direction.RIGHT;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (lastDir != ProjectileMove.Direction.RIGHT)
            {
            	Back2Normal();
            	lastDir = ProjectileMove.Direction.RIGHT;
            }
            transform.Translate(Vector3.right*Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (lastDir != ProjectileMove.Direction.LEFT)
            {
            	Back2Normal();
            	transform.Rotate(0.0f, 0.0f, -180.0f, Space.Self);
            	lastDir =  ProjectileMove.Direction.LEFT;
            }
            transform.Translate(Vector3.right*Time.deltaTime*Speed);

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {  
            if (lastDir != ProjectileMove.Direction.UP)
            {
            	Back2Normal();
            	transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            	lastDir = ProjectileMove.Direction.UP;
            }
            transform.Translate(Vector3.right*Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (lastDir != ProjectileMove.Direction.DOWN)
            {
            	Back2Normal();
            	transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
            	lastDir = ProjectileMove.Direction.DOWN;
            }
            transform.Translate(Vector3.right*Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            triggerZone.TriggerAction(lastDir);
        }
    }

    private void Back2Normal()
    {
    	if (lastDir == ProjectileMove.Direction.LEFT)
    	{
    		transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
    	}
    	if (lastDir == ProjectileMove.Direction.UP)
    	{
    		transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
    	}
    	if (lastDir == ProjectileMove.Direction.DOWN)
    	{
    		transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
    	}
    	lastDir = ProjectileMove.Direction.RIGHT;
    }
}
