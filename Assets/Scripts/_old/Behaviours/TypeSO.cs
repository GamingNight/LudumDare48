using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Data Type")]
public class TypeSO : ScriptableObject
{
    public string typeName;

    private List<Type> allTypes;
    public Type FindType() {

        Type res = Type.GetType(typeName);
        if (res == null) {
            if (allTypes == null)
                allTypes = GetAllTypes();
            List<Type> allRes = new List<Type>();
            foreach (Type type in allTypes) {
                if (type.Name == typeName || type.FullName == typeName)
                    allRes.Add(type);
            }
            res = allRes.Count == 0 ? null : allRes[0];
        }
        return res;
    }

    private List<Type> GetAllTypes() {
        List<Type> res = new List<Type>();
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
            res.AddRange(assembly.GetTypes());
        }
        return res;
    }

    public object DrawPropertyField(Rect position, object inValue) {
        object outValue = null;
        Type type = FindType();
        if (type == typeof(float)) {
            outValue = EditorGUI.FloatField(position, (float)inValue);
        } else if (type == typeof(int)) {
            outValue = EditorGUI.IntField(position, (int)inValue);
        } else if (type == typeof(bool)) {
            outValue = EditorGUI.Toggle(position, (bool)inValue);
        } else if (type == typeof(string)) {
            outValue = EditorGUI.TextField(position, (string)inValue);
        } else if (type == typeof(Vector2)) {
            outValue = EditorGUI.Vector2Field(position, GUIContent.none, (Vector2)inValue);
        } else if (type == typeof(Vector3)) {
            outValue = EditorGUI.Vector3Field(position, GUIContent.none, (Vector3)inValue);
        } else {
            EditorGUI.LabelField(position, "Data type not supported");
        }
        return outValue;
    }
}
