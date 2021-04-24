using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneController : MonoBehaviour
{

	private List<ProjectileMove> projectileList;

    // Start is called before the first frame update
    void Start()
    {
        projectileList = new List<ProjectileMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

	public void TriggerAction(string lastDir)
	{
		foreach(ProjectileMove projectile in projectileList)
		{
    		if (lastDir == "right")
    		{
    			projectile.HitProjectile(ProjectileMove.Direction.RIGHT);
    		}
    		if (lastDir == "left")
    		{
    			projectile.HitProjectile(ProjectileMove.Direction.LEFT);
    		}
    		if (lastDir == "up")
	    	{
	    		projectile.HitProjectile(ProjectileMove.Direction.UP);
	    	}
	    	if (lastDir == "down")
	    	{
	    		projectile.HitProjectile(ProjectileMove.Direction.DOWN);
	    	}
		}
	}
}
