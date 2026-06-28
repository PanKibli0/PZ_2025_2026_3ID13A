using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;

public abstract class BaseSerializeReferenceDrawer : PropertyDrawer
{
    protected virtual bool IsValidType(Type type) => true;

    private Type[] cachedTypes;
    private string[] cachedTypeNames;

    private Type[] GetTypes()
    {
        if (cachedTypes != null) return cachedTypes;

        Type baseType = fieldInfo.FieldType;

        cachedTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a =>
            {
                try { return a.GetTypes(); }
                catch { return Array.Empty<Type>(); }
            })
            .Where(t =>
                !t.IsAbstract &&
                !t.IsInterface &&
                !t.IsGenericTypeDefinition &&
                baseType.IsAssignableFrom(t) &&
                t != baseType &&
                t.GetConstructor(Type.EmptyTypes) != null &&
                IsValidType(t))
            .OrderBy(t => t.Name)
            .ToArray();

        return cachedTypes;
    }

    private string[] GetTypeNames()
    {
        if (cachedTypeNames != null) return cachedTypeNames;
        cachedTypeNames = GetTypes().Select(t => t.Name).ToArray();
        return cachedTypeNames;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Type[] types = GetTypes();
        string[] typeNames = GetTypeNames();

        Type currentType = property.managedReferenceValue?.GetType();
        int selectedIndex = currentType != null ? Array.IndexOf(types, currentType) : -1;

        string[] displayNames = new string[] { "-- None --" }.Concat(typeNames).ToArray();
        int displayIndex = selectedIndex + 1;

        Rect popupRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        int newDisplayIndex = EditorGUI.Popup(popupRect, label.text, displayIndex, displayNames);

        if (newDisplayIndex != displayIndex)
        {
            property.managedReferenceValue = newDisplayIndex == 0
                ? null
                : Activator.CreateInstance(types[newDisplayIndex - 1]);
            property.serializedObject.ApplyModifiedProperties();
        }

        if (property.managedReferenceValue != null)
        {
            EditorGUI.indentLevel++;
            float childHeight = EditorGUI.GetPropertyHeight(property, true);
            Rect childRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, childHeight);
            EditorGUI.PropertyField(childRect, property, GUIContent.none, true);
            EditorGUI.indentLevel--;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.managedReferenceValue != null)
            return EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.singleLineHeight + 4;

        return EditorGUIUtility.singleLineHeight;
    }
}