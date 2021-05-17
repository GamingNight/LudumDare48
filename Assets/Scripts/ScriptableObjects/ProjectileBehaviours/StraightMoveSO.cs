using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMoveSO : AbstractProjectileBehaviourSO
{
    public Vector2 direction;

    private Rigidbody2D rgbd;
    private ProjectileMove projectileScript;

    public override void InitiateMove(GameObject projectile) {

        projectileScript = projectile.GetComponent<ProjectileMove>();
        rgbd = projectile.GetComponent<Rigidbody2D>();

        ProjectileDefaultDataSO defaultData = projectileScript.defaultData;
        ProjectileData customData = projectileScript.customData;
        if (customData.initForceFactor == 0)
            customData.initForceFactor = defaultData.initForceFactor;
        rgbd.AddForce(direction * customData.initForceFactor);
    }

    public override void UpdateMove(GameObject projectile) {
        //Nothing, everything is done in the initialization
    }
}
