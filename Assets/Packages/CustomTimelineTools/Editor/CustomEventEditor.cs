using Shinn.Timelinie;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomEventPlayable))]
public class CustomEventEditor : Editor
{
    CustomEventPlayable myScript;
    private int selected = 0;

    public override void OnInspectorGUI()
    {
        myScript = (CustomEventPlayable)target;
        selected = myScript.selected;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("target"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("type"));

        if (myScript.MethodList == null)
            return;

        string[] options = myScript.MethodList.ToArray();
        selected = EditorGUILayout.Popup("Method", selected, options);

        switch (myScript.type)
        {
            case CustomEventClip.ParameterType.NULL:
                break;
            case CustomEventClip.ParameterType.VOID:
                if (options.Length != 0) 
                    myScript.Method = options[selected];
                break;
            case CustomEventClip.ParameterType.INT:
                if (options.Length != 0)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("IntInput"));
                    myScript.Method = options[selected];
                }
                break;
            case CustomEventClip.ParameterType.FLOAT:
                if (options.Length != 0)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("FloatInput"));
                    myScript.Method = options[selected];
                }
                break;
            case CustomEventClip.ParameterType.STRING:
                if (options.Length != 0)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("StringInput"));
                    myScript.Method = options[selected];
                }
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
