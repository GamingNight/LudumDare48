using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneController : MonoBehaviour
{

	private List<ProjectileMove> projectileList;
	private ProjectileMove.Direction lastDir = ProjectileMove.Direction.RIGHT;

    // Start is called before the first frame update
    void Start()
    {
        projectileList = new List<ProjectileMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (lastDir != ProjectileMove.Direction.RIGHT)
            {
            	float angle = Back2NormalAngle();
            	transform.Rotate(0.0f, 0.0f, angle, Space.Self);
            	lastDir = ProjectileMove.Direction.RIGHT;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (lastDir != ProjectileMove.Direction.LEFT)
            {
            	float angle = Back2NormalAngle() + 180.0f;
            	transform.Rotate(0.0f, 0.0f, angle, Space.Self);
            	lastDir =  ProjectileMove.Direction.LEFT;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {  
            if (lastDir != ProjectileMove.Direction.UP)
            {
            	float angle = Back2NormalAngle() + 90.0f;
            	transform.Rotate(0.0f, 0.0f, angle, Space.Self);
            	lastDir = ProjectileMove.Direction.UP;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (lastDir != ProjectileMove.Direction.DOWN)
            {
            	float angle = Back2NormalAngle() - 90.0f;
            	transform.Rotate(0.0f, 0.0f, angle, Space.Self);
            	lastDir = ProjectileMove.Direction.DOWN;
            }
        }

        //if (Input.GetKey(KeyCode.Space))
        //{
		    foreach(ProjectileMove projectile in projectileList)
		    {
			    projectile.HitProjectile(lastDir);
		    }
        //}
    }

	void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Projectile") {
            projectileList.Add(collision.gameObject.GetComponent<ProjectileMove>());
        }
	}

	void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Projectile") {
            projectileList.Remove(collision.gameObject.GetComponent<ProjectileMove>());
        }
	}
    private float Back2NormalAngle()
    {
    	//return;
    	if (lastDir == ProjectileMove.Direction.LEFT)
    	{
    		return -180.0f;
    	}
    	if (lastDir == ProjectileMove.Direction.UP)
    	{
    		return -90.0f;
    	}
    	if (lastDir == ProjectileMove.Direction.DOWN)
    	{
    		return 90.0f;
    	}
    	return 0.0f;
    }
}
