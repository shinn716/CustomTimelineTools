using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Shinn.Timelinie;
using System.Linq;
using System.Reflection;
using System;
using System.Text.RegularExpressions;

[CustomEditor(typeof(CustomEventClip))]
public class CustomEventControlPlayableEditor : Editor
{
    public enum Options
    {
        Void,
        Parameter_Int,
        Parameter_Float,
        Parameter_String
    }

    public Options options;
    public int label = 0;

    private CustomEventClip script;

    private SerializedProperty type;
    private SerializedProperty input_str;
    private SerializedProperty input_int;
    private SerializedProperty input_float;
    private SerializedProperty useClipDuring;

    GUIContent label_input_str;
    GUIContent label_input_int;
    GUIContent label_input_float;
    GUIContent label_useClipDuring;

    private List<string> _eventHandlerListStart = new List<string> { };

    public void OnEnable()
    {
        script = (CustomEventClip)target;
        type = serializedObject.FindProperty("template.type");
        input_str = serializedObject.FindProperty("template.input_str");
        input_int = serializedObject.FindProperty("template.input_int");
        input_float = serializedObject.FindProperty("template.input_float");
        useClipDuring = serializedObject.FindProperty("template.useClipDuring");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        label = EditorPrefs.GetInt("label");

        switch (EditorPrefs.GetString("options"))
        {
            default:
                options = Options.Void;
                break;
            case "Parameter_Int":
                options = Options.Parameter_Int;
                break;
            case "Parameter_Float":
                options = Options.Parameter_Float;
                break;
            case "Parameter_String":
                options = Options.Parameter_String;
                break;
        }

        options = (Options)EditorGUILayout.EnumPopup("Types:", options);

        switch (options)
        {
            default:
                script.template.type = CustomEventControlPlayable.ParameterType.Void;
                break;
            case Options.Parameter_Int:
                script.template.type = CustomEventControlPlayable.ParameterType.Int;
                break;
            case Options.Parameter_String:
                script.template.type = CustomEventControlPlayable.ParameterType.String;
                break;
            case Options.Parameter_Float:
                script.template.type = CustomEventControlPlayable.ParameterType.Float;
                break;
        }

        // Experiment
        var _methods = script.template.targetEventmanager.GetComponents<Behaviour>();

        var allMethods = _methods.SelectMany(
                x => x.GetType()
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance))
            .Where(
                x =>
                {
                    if (options == Options.Parameter_Int)
                        return (x.ReturnType == typeof(void)) && (x.GetParameters().Length == 1) &&
                               (x.GetParameters()[0].ParameterType == typeof(int));

                    else if (options == Options.Parameter_Float)
                        return (x.ReturnType == typeof(void)) && (x.GetParameters().Length == 1) &&
                               (x.GetParameters()[0].ParameterType == typeof(float));
                    else if (options == Options.Parameter_String)
                        return (x.ReturnType == typeof(void)) && (x.GetParameters().Length == 1) &&
                               (x.GetParameters()[0].ParameterType == typeof(string));
                    else
                        return (x.ReturnType == typeof(void)) && (x.GetParameters().Length == 0);
                }).ToArray();
        
        var callbackMethodsEnumarable = allMethods.Select(
        x => x.DeclaringType.ToString() + "." + x.Name);
        
        string[] callbackMethods = _eventHandlerListStart.Concat(callbackMethodsEnumarable).ToArray();
        var lastTwoDotPattern = @"[^\.]+\.[^\.]+$";

        var callbackMethodNames = callbackMethods.Select(m =>
        {
            var result = Regex.Match(m, lastTwoDotPattern, RegexOptions.RightToLeft);
            return result.Success ? result.Value : m;
        }).ToArray();
        
        label = EditorGUILayout.Popup(label, callbackMethodNames, GUILayout.ExpandWidth(true));
        script.template.HandlerKey = callbackMethodNames[label];

        EditorGUILayout.Space();

        switch (options)
        {
            case Options.Parameter_Float:
                EditorGUILayout.PropertyField(useClipDuring, label_useClipDuring);
                if (!script.template.useClipDuring)
                    EditorGUILayout.PropertyField(input_float, label_input_float);
                break;
            case Options.Parameter_Int:
                EditorGUILayout.PropertyField(input_int, label_input_int);
                break;
            case Options.Parameter_String:
                EditorGUILayout.PropertyField(input_str, label_input_str);
                break;
        }

        EditorPrefs.SetString("options", options.ToString());
        EditorPrefs.SetInt("label", label);
        serializedObject.ApplyModifiedProperties();
    }

}
