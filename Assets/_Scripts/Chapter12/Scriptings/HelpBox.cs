using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.UserInterface
{
#if UNITY_EDITOR
    using UnityEditor;
#endif
    public class HelpBoxAttribute : PropertyAttribute
    {
        public string text;
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(HelpBoxAttribute))]
    public class HelpBoxAttributePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var helpBoxPosition = position;
            helpBoxPosition.height = HelpBoxHeight;
            var propertyPosition = position;
            propertyPosition.y += EditorGUIUtility.standardVerticalSpacing
             + helpBoxPosition.height;
            propertyPosition.height =
                EditorGUI.GetPropertyHeight(property, includeChildren: true);
            HelpBoxAttribute helpBox = (attribute as HelpBoxAttribute);
            string text = helpBox.text;
            EditorGUI.HelpBox(helpBoxPosition, text, MessageType.Info);
            EditorGUI.PropertyField(
                propertyPosition, property, includeChildren: true
            );
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float lineSpacing = EditorGUIUtility.standardVerticalSpacing;
            float propertyHeight =
                    EditorGUI.GetPropertyHeight(property, includeChildren: true);
            return HelpBoxHeight + lineSpacing + propertyHeight;
        }
        private float HelpBoxHeight
        {
            get
            {
                var width = EditorGUIUtility.currentViewWidth;
                var helpBoxAttribute = attribute as HelpBoxAttribute;
                var content = new GUIContent(helpBoxAttribute.text);
                var helpBoxHeight =
                    EditorStyles.helpBox.CalcHeight(content, width);
                return helpBoxHeight + EditorGUIUtility.singleLineHeight;
            }
        }
    }
#endif

}
