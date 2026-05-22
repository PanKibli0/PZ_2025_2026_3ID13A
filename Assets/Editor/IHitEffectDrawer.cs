using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(IHitEffect), true)]
public class IHitEffectDrawer : PropertyDrawer
{
    private Type[] types;
    private string[] typeNames;

    public IHitEffectDrawer()
    {
        types = new Type[]
        {
            typeof(HealHitEffect),
            typeof(KnockbackHitEffect)
            
        };

        typeNames = new string[types.Length];
        for (int i = 0; i < types.Length; i++)
            typeNames[i] = types[i].Name;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Type currentType = property.managedReferenceValue?.GetType();
        int selectedIndex = -1;

        if (currentType != null)
        {
            for (int i = 0; i < types.Length; i++)
            {
                if (types[i] == currentType)
                {
                    selectedIndex = i;
                    break;
                }
            }
        }

        Rect popupRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        int newIndex = EditorGUI.Popup(popupRect, label.text, selectedIndex, typeNames);

        if (newIndex != selectedIndex && newIndex != -1)
        {
            property.managedReferenceValue = Activator.CreateInstance(types[newIndex]);
            property.serializedObject.ApplyModifiedProperties();
        }

        if (property.managedReferenceValue != null)
        {
            EditorGUI.indentLevel++;
            float height = EditorGUI.GetPropertyHeight(property, true);
            Rect childRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, height);
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