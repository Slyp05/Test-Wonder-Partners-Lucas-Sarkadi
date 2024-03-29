using System;
using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUI;

/// <summary>
/// <see cref="PropertyDrawer"/> for a <see cref="TransformState"/>.
/// </summary>
[CustomPropertyDrawer(typeof(TransformState))]
class TransformStateDrawer : PropertyDrawer
{
    static readonly GUIContent CopyCurrentButtonText = new GUIContent("Copy", "Set all serialized values by copying the current transform local values.");
    static readonly GUIContent PasteValuesButtonText = new GUIContent("Paste", "Set the current transform local values by pasting all serialized values.");

    struct Properties
    {
        public readonly SerializedProperty parent;

        public readonly SerializedProperty position;
        public readonly SerializedProperty eulerAngles;
        public readonly SerializedProperty scale;

        public Properties(SerializedProperty parent)
        {
            this.parent = parent;

            position = parent.FindPropertyRelative(nameof(position));
            eulerAngles = parent.FindPropertyRelative(nameof(eulerAngles));
            scale = parent.FindPropertyRelative(nameof(scale));
        }
    }

    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        Properties properties = new Properties(property);

        label = BeginProperty(rect, label, property);
        {
            rect.height = EditorGUIUtility.singleLineHeight;

            Rect labelRect = new Rect(rect) { width = EditorGUIUtility.labelWidth };
            LabelField(labelRect, label);

            Rect buttonRect = new Rect(rect) { xMin = labelRect.width + EditorGUIUtility.standardVerticalSpacing };
            buttonRect.width = (buttonRect.width - EditorGUIUtility.standardVerticalSpacing) / 2f;
            if (GUI.Button(buttonRect, CopyCurrentButtonText))
                CopyProperties(properties);
            buttonRect.x += buttonRect.width + EditorGUIUtility.standardVerticalSpacing;
            if (GUI.Button(buttonRect, PasteValuesButtonText))
                PasteProperties(properties);

            DecalRect();

            ++indentLevel;

            rect.height = EditorGUI.GetPropertyHeight(properties.position);
            PropertyField(rect, properties.position);
            DecalRect();

            rect.height = EditorGUI.GetPropertyHeight(properties.eulerAngles);
            PropertyField(rect, properties.eulerAngles);
            DecalRect();

            rect.height = EditorGUI.GetPropertyHeight(properties.scale);
            PropertyField(rect, properties.scale);

            --indentLevel;
        }
        EndProperty();

        void DecalRect() => rect.y += rect.height + EditorGUIUtility.standardVerticalSpacing;
    }

    void CopyProperties(Properties properties)
    {
        Transform transform = GetTransform(properties.parent);

        properties.position.vector3Value = transform.localPosition;
        properties.eulerAngles.vector3Value = transform.localEulerAngles;
        properties.scale.vector3Value = transform.localScale;
    }

    void PasteProperties(Properties properties)
    {
        Transform transform = GetTransform(properties.parent);

        transform.localPosition = properties.position.vector3Value;
        transform.localEulerAngles = properties.eulerAngles.vector3Value;
        transform.localScale = properties.scale.vector3Value;
    }

    Transform GetTransform(SerializedProperty property)
    {
        MonoBehaviour monoBehaviour = property.serializedObject.targetObject as MonoBehaviour;
        if (monoBehaviour == null)
            throw new ApplicationException($"Cannot set values of '{nameof(TransformStateDrawer)}' because it is not a {nameof(MonoBehaviour)} field.");

        return monoBehaviour.transform;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Properties properties = new Properties(property);

        return EditorGUIUtility.singleLineHeight
             + EditorGUI.GetPropertyHeight(properties.position)
             + EditorGUI.GetPropertyHeight(properties.eulerAngles)
             + EditorGUI.GetPropertyHeight(properties.scale)
             + EditorGUIUtility.standardVerticalSpacing * 3f;
    }
}
