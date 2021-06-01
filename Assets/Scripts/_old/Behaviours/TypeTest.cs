using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeTest : MonoBehaviour
{
    public TypeSO type;
    void Start() {
        Debug.Log("Type is " + type.FindType());
        //Debug.Log(type.FindType() == typeof(string));
    }
}
