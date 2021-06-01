using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBehaviourSO : ScriptableObject
{
    public abstract void SetParameters(Parameter[] parameters, object[] values);

    public abstract object[] GetParameters(Parameter[] parameters);
}
