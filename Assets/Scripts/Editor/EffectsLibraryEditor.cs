using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EffectsLibrary))]
public class EffectsLibraryEditor : Editor
{
    private SerializedProperty decalLibraryProperty;
    private SerializedProperty particleLibraryProperty;

    private void OnEnable()
    {
        decalLibraryProperty = serializedObject.FindProperty("decalLibrary");
        particleLibraryProperty = serializedObject.FindProperty("particleLibrary");
    }

    private void MapIndexToEnum(string _header, SerializedProperty _property, string[] _labels)
    {
        if (_property.isExpanded) {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField(_header, EditorStyles.boldLabel);
            for (int i = 0; i < _property.arraySize; i++) {
                SerializedProperty elementProperty = _property.GetArrayElementAtIndex(i);

                string label = _labels[i];
                EditorGUILayout.PropertyField(elementProperty, new GUIContent(label));
            }
            EditorGUI.indentLevel--;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(decalLibraryProperty);
        EditorGUILayout.PropertyField (particleLibraryProperty);

        MapIndexToEnum("Decal Library", decalLibraryProperty, Enum.GetNames(typeof(DecalType)));
        MapIndexToEnum("Particle Library", particleLibraryProperty, Enum.GetNames(typeof(ParticleEffectType)));

        serializedObject.ApplyModifiedProperties();
    }
}
