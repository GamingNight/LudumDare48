using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectileHit : MonoBehaviour
{

    private void Update() {

        if (Input.GetKeyDown(KeyCode.RightArrow))
            GetComponent<ProjectileMove>().HitProjectile(ProjectileMove.Direction.RIGHT);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            GetComponent<ProjectileMove>().HitProjectile(ProjectileMove.Direction.LEFT);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            GetComponent<ProjectileMove>().HitProjectile(ProjectileMove.Direction.UP);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            GetComponent<ProjectileMove>().HitProjectile(ProjectileMove.Direction.DOWN);
    }
}
