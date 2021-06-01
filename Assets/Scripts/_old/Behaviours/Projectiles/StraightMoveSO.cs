using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Behaviours/Projectiles/Straight Move")]
public class StraightMoveSO : AbstractProjectileBehaviourSO
{
    public float initForceFactor;
    private Rigidbody2D rgbd;
    private ProjectileStraightMove projectileScript;

    public override void InitiateMove(GameObject projectile) {

        projectileScript = projectile.GetComponent<ProjectileStraightMove>();
        rgbd = projectile.GetComponent<Rigidbody2D>();

        ProjectileDefaultDataSO defaultData = projectileScript.defaultData;
        if (initForceFactor == 0)
            initForceFactor = defaultData.initForceFactor;
        rgbd.AddForce(projectileScript.direction * initForceFactor);
    }

    public override void UpdateMove(GameObject projectile) {
        //Nothing, all has been done in the initialization
    }

    public override void SetParameters(Parameter[] parameters, object[] values) {

        for (int i = 0; i < parameters.Length; i++) {
            if (parameters[i].name == "initForceFactor") {
                initForceFactor = (float)values[i];
            }
        }
    }

    public override object[] GetParameters(Parameter[] parameters) {

        object[] values = new object[parameters.Length];
        for (int i = 0; i < parameters.Length; i++) {
            if (parameters[i].name == "initForceFactor") {
                values[i] = initForceFactor;
            }
        }
        return values;
    }
}
