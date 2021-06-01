using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

[System.Serializable]
[CreateAssetMenu(menuName = "Scriptable Objects/Behaviours/Behaviour Params")]
public class BehaviourParamListSO : ScriptableObject
{
    public TypeSO behaviourType;
    public Parameter[] parameters;

    public AbstractBehaviourSO InstantiateBehaviour(object[] paramValues) {
        AbstractBehaviourSO behaviour = (AbstractBehaviourSO)ScriptableObject.CreateInstance(behaviourType.FindType());
        behaviour.SetParameters(parameters, paramValues);
        return behaviour;
    }
}
