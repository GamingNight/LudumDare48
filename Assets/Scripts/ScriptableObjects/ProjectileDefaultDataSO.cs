using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Projectile Data")]
public class ProjectileDefaultDataSO : ScriptableObject
{
    /// <summary>
    /// Initial force multiplier impulsed to the projectile in order to make it move.
    /// </summary>
    public float initForceFactor;

    /// <summary>
    /// For each layer, translation speed in units per second when projectile is hit by the player
    /// </summary>
    public float[] hitTranslationSpeedPerLayer;

    /// <summary>
    /// How much in units the projectile is moved when it is hit by the player
    /// </summary>
    public float hitTranslationLenght;

}
