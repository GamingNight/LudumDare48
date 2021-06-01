using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectileBehaviourSO : AbstractBehaviourSO
{
    public abstract void InitiateMove(GameObject projectile);
    public abstract void UpdateMove(GameObject projectile);
}
