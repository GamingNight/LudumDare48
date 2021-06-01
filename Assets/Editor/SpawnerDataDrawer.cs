using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

//[CustomPropertyDrawer(typeof(SpawnerData))]
public class SpawnerDataDrawer : PropertyDrawer
{
    private BehaviourParamListSO customProjectileParams;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

        /*if (customProjectileBehaviour == null || customProjectileBehaviour.GetType() != customProjectileParams.behaviourType.FindType()) {
            customProjectileBehaviour = (AbstractProjectileBehaviourSO)ScriptableObject.CreateInstance(customProjectileParams.behaviourType.FindType());
        }
        */

        float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        if (property.isExpanded) {
            height += (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 10;

            SerializedProperty paramProperty = property.FindPropertyRelative("customProjectileBehaviourParams");
            customProjectileParams = paramProperty.objectReferenceValue as System.Object as BehaviourParamListSO;
            if (customProjectileParams != null) {
                height += customProjectileParams.parameters.Length * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
            }
        }
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

        EditorGUI.BeginProperty(position, label, property);

        //Fold out arrow
        Rect fouldOutPos = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(fouldOutPos, property.isExpanded, label);
        if (property.isExpanded) {
            //Spawner position
            Rect positionPos = new Rect(position.x, fouldOutPos.y + fouldOutPos.height + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(positionPos, property.FindPropertyRelative("position"));

            //Spawner rotation
            Rect rotationPos = new Rect(position.x, positionPos.y + positionPos.height + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(rotationPos, property.FindPropertyRelative("rotation"));

            //Frequency
            Rect frequencyPos = new Rect(position.x, rotationPos.y + rotationPos.height + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(frequencyPos, property.FindPropertyRelative("frequency"));

            //Offset in seconds
            Rect offsetPos = new Rect(position.x, frequencyPos.y + frequencyPos.height + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(offsetPos, property.FindPropertyRelative("offsetInSeconds"));

            //Init force factor
            Rect initForceFactorPos = new Rect(position.x, offsetPos.y + offsetPos.height + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(initForceFactorPos, property.FindPropertyRelative("initForceFactor"));

            //Populate
            Rect populatePos = new Rect(position.x, initForceFactorPos.y + initForceFactorPos.height + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(populatePos, property.FindPropertyRelative("populate"));

            //Projectile behaviour
            //Label
            Rect labelPosition = new Rect(position.x, populatePos.y + populatePos.height + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(labelPosition, "Projectile behaviour");

            EditorGUI.indentLevel++;
            //Default behaviour
            Rect indentPosition = EditorGUI.IndentedRect(position);
            Rect defaultBehaviourPos = new Rect(indentPosition.x, labelPosition.y + labelPosition.height + EditorGUIUtility.standardVerticalSpacing, indentPosition.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(defaultBehaviourPos, property.FindPropertyRelative("defaultProjectileBehaviour"), new GUIContent("Default"));

            //or
            Rect orLabelPosition = new Rect(indentPosition.x, defaultBehaviourPos.y + defaultBehaviourPos.height + EditorGUIUtility.standardVerticalSpacing, indentPosition.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(orLabelPosition, "or");

            //Custom params
            SerializedProperty customProjectileParamsProperty = property.FindPropertyRelative("customProjectileBehaviourParams");
            Rect customParamsPosition = new Rect(indentPosition.x, orLabelPosition.y + orLabelPosition.height + EditorGUIUtility.standardVerticalSpacing, indentPosition.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(customParamsPosition, customProjectileParamsProperty, new GUIContent("Custom Params"));

            if (customProjectileParams != null) {
                EditorGUI.indentLevel++;
                Rect doubleIdentdPosition = EditorGUI.IndentedRect(indentPosition);
                SerializedProperty prop = property.FindPropertyRelative("customProjectileBehaviour");
                AbstractProjectileBehaviourSO customBehaviour = prop as System.Object as AbstractProjectileBehaviourSO;
                if (customBehaviour == null) {
                    customBehaviour = (AbstractProjectileBehaviourSO)ScriptableObject.CreateInstance(customProjectileParams.behaviourType.FindType());
                }
                object[] values = customBehaviour.GetParameters(customProjectileParams.parameters);
                for (int i = 0; i < customProjectileParams.parameters.Length; i++) {
                    Parameter p = customProjectileParams.parameters[i];
                    //Parameter label
                    Rect paramLabelPosition = new Rect(doubleIdentdPosition.x, customParamsPosition.y + ((i + 1) * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing)), EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
                    EditorGUI.LabelField(paramLabelPosition, p.GetInspectorName());

                    //Parameter value
                    Rect valueRect = new Rect(paramLabelPosition.x + paramLabelPosition.width, paramLabelPosition.y, doubleIdentdPosition.width - paramLabelPosition.width, EditorGUIUtility.singleLineHeight);
                    values[i] = p.type.DrawPropertyField(valueRect, values[i]);
                }
                customBehaviour.SetParameters(customProjectileParams.parameters, values);
                prop.objectReferenceValue = customBehaviour;
                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
        }
        EditorGUI.EndProperty();
    }
}
