using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectileBehaviourSO : ScriptableObject
{
    public abstract void InitiateMove(GameObject projectile);
    public abstract void UpdateMove(GameObject projectile);
}
