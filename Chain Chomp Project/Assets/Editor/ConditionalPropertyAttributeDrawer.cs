using System;
using UnityEngine;
using UnityEditor;
/// <summary>
/// Note: Doesn't work on arrays or lists because of how Unity works. It can hide the elements of the array, but not the array property itself.
/// </summary>
[CustomPropertyDrawer(typeof(ConditionalPropertyAttribute))]
public class ConditionalPropertyAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        bool isVisible = IsVisible(property);
        if (isVisible)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndProperty();
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Make the height 0 if we aren't drawing the property, otherwise a blank space appears in the inspector
        bool isVisible = IsVisible(property);
        float height = 0;
        if (isVisible)
        {
            height = EditorGUI.GetPropertyHeight(property);
        }
        return height;
    }
    private bool IsVisible(SerializedProperty property)
    {
        // Extract info from the attribute
        var conditionalPropertyAttribute = this.attribute as ConditionalPropertyAttribute;
        if (conditionalPropertyAttribute == null)
        {
            Debug.LogError("ConditionPropertyAttributeDrawer couldn't cast attribute to ConditionalPropertyAttribute");
        }
        string propertyToCheckName = conditionalPropertyAttribute.propertyToCheck;
        string requiredValue = "null";
        if (conditionalPropertyAttribute.compareValue != null)
        {
            requiredValue = conditionalPropertyAttribute.compareValue.ToString();
        }
        bool isVisible = true; // Default to visible if the logic isn't working out (properties can't be found, etc)
        // Determine if we need to hide the property
        if (!string.IsNullOrEmpty(propertyToCheckName))
        {
            // Make sure we can find the property it depends on
            var propertyToCheck = property.serializedObject.FindProperty(propertyToCheckName);
            if (propertyToCheck != null)
            {
                //string actualValue = propertyToCheck.AsStringValue();
                // See if the values match
               // isVisible = string.Equals(requiredValue, actualValue, StringComparison.OrdinalIgnoreCase);
            }
        }
        return isVisible;
    }
}