using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Layer")]
public class LayerDataSO : ScriptableObject
{

    public int layerIndex;
    public float[] layerSpeedFactors;
}
