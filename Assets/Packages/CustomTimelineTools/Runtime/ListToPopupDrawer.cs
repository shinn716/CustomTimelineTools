using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace Shinn.Timelinie
{
    public class ListToPopupAttribute : PropertyAttribute
    {

        public Type type;
        public string propertyName;

        public ListToPopupAttribute(Type _type, string _propertyName)
        {
            type = _type;
            propertyName = _propertyName;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ListToPopupAttribute))]
    public class ListToPopupDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ListToPopupAttribute atb = attribute as ListToPopupAttribute;
            List<string> stringList = null;

            if (atb.type.GetField(atb.propertyName) != null)
            {
                stringList = atb.type.GetField(atb.propertyName).GetValue(atb.type) as List<string>;
            }

            if (stringList != null && stringList.Count != 0)
            {
                int selectedIndex = Mathf.Max(stringList.IndexOf(property.stringValue), 0);
                selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, stringList.ToArray());
                property.stringValue = stringList[selectedIndex];
            }
            else EditorGUI.PropertyField(position, property, label);
        }
    }
#endif
}
