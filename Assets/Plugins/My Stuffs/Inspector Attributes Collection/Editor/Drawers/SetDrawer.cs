﻿using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorAttribute
{
    [CustomPropertyDrawer(typeof(SetAttribute))]
    class SetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (this.RepresentAnArray())
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            SetAttribute attribute = (SetAttribute)base.attribute;

            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, label);
            if (EditorGUI.EndChangeCheck())
            {
                attribute.dirty = true;
            }
            else if (attribute.dirty)
            {
                var parent = GetParentObject(property.propertyPath, property.serializedObject.targetObject);

                var type = parent.GetType();
                var info = type.GetProperty(attribute.name);

                if (info == null)
                    Debug.LogError("Invalid property name \"" + attribute.name + "\"");
                else
                    info.SetValue(parent, fieldInfo.GetValue(parent), null);

                attribute.dirty = false;
            }
        }

        public static object GetParentObject(string path, object obj)
        {
            var fields = path.Split('.');

            if (fields.Length == 1)
                return obj;

            FieldInfo info = obj.GetType().GetField(fields[0], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            obj = info.GetValue(obj);

            return GetParentObject(string.Join(".", fields, 1, fields.Length - 1), obj);
        }
    }
}