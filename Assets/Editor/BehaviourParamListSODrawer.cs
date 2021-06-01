using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomPropertyDrawer(typeof(BehaviourParamListSO))]
public class BehaviourParamListSODrawer : PropertyDrawer
{

    private Parameter[] parameters = null;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

        float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        BehaviourParamListSO projectileParams = property.objectReferenceValue as System.Object as BehaviourParamListSO;
        if (projectileParams != null) {
            parameters = projectileParams.parameters;
            height += parameters.Length * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
        }
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PropertyField(position, property, label, true);

        if (parameters != null) {
            int prevIndent = EditorGUI.indentLevel;
            Rect indentedPosition = EditorGUI.IndentedRect(position);
            EditorGUI.indentLevel += 1;
            object[] values = new object[parameters.Length];
            int i = 0;
            foreach (Parameter p in parameters) {
                //Parameter label
                Rect paramLabelPosition = new Rect(indentedPosition.x, position.y + ((i + 1) * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing)), EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(paramLabelPosition, p.GetInspectorName());

                //Parameter value
                Rect valueRect = new Rect(paramLabelPosition.x + paramLabelPosition.width, paramLabelPosition.y, indentedPosition.width - paramLabelPosition.width, EditorGUIUtility.singleLineHeight);
                Type type = p.type.FindType();
                //values[i] = p.type.DrawPropertyField(valueRect);
                i++;
            }
            EditorGUI.indentLevel = prevIndent;
            //BehaviourParamListSO.RegisterParamValues(position, values);
        }
        EditorGUI.EndProperty();
    }
    public BehaviourParamListSODrawer GetDrawer() {

        return this;
    }
}
