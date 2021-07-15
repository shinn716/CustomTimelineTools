using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Shinn.Timelinie;

[CustomEditor(typeof(CustomTimelineEventManager)), CanEditMultipleObjects]
public class CustomTimelineEventManagerEditor : Editor
{
    public enum OPTIONS
    {
        ON_START_Int,
        ON_START_Float,
        ON_START_String,
        ON_END_Int,
        ON_END_Float,
        ON_END_String
    }

    public OPTIONS op;

    private CustomTimelineEventManager script;

    private SerializedProperty onStartEvents_INT;
    private SerializedProperty onStartEvents_FLOAT;
    private SerializedProperty onStartEvents_STRING;

    private SerializedProperty onEndEvents_INT;
    private SerializedProperty onEndEvents_FLOAT;
    private SerializedProperty onEndEvents_STRING;

    private GUIContent label_onStartEvents_INT;
    private GUIContent label_onStartEvents_FLOAT;
    private GUIContent label_onStartEvents_STRING;

    private GUIContent label_onEndEvents_INT;
    private GUIContent label_onEndEvents_FLOAT;
    private GUIContent label_onEndEvents_STRING;

    private bool m_start_INT = false;
    private bool m_start_FLOAT = false;
    private bool m_start_STRING = false;
    private bool m_end_INT = false;
    private bool m_end_FLOAT = false;
    bool m_end_STRING = false;

    public void OnEnable()
    {
        script = (CustomTimelineEventManager) target;
        onStartEvents_INT = serializedObject.FindProperty("onStartEvents_INT");
        onStartEvents_FLOAT = serializedObject.FindProperty("onStartEvents_FLOAT");
        onStartEvents_STRING = serializedObject.FindProperty("onStartEvents_STRING");

        onEndEvents_INT = serializedObject.FindProperty("onEndEvents_INT");
        onEndEvents_FLOAT = serializedObject.FindProperty("onEndEvents_FLOAT");
        onEndEvents_STRING = serializedObject.FindProperty("onEndEvents_STRING");

        label_onStartEvents_INT = new GUIContent("onStartEvents_INT");
    }
    
    public override void OnInspectorGUI()
    {
        op = (OPTIONS) EditorGUILayout.EnumPopup("Types:", op);
        if (GUILayout.Button("Create UnityEvent"))
        {
            switch (op)
            {
                case OPTIONS.ON_START_Int:
                    m_start_INT = true;
                    break;
                case OPTIONS.ON_START_Float:
                    m_start_FLOAT = true;
                    break;
                case OPTIONS.ON_START_String:
                    m_start_STRING = true;
                    break;
                case OPTIONS.ON_END_Int:
                    m_end_INT = true;
                    break;
                case OPTIONS.ON_END_Float:
                    m_end_FLOAT = true;
                    break;
                case OPTIONS.ON_END_String:
                    m_end_STRING = true;
                    break;
            }
        }
        EditorGUILayout.Space();

        if (m_start_INT || m_start_FLOAT || m_start_STRING)
        {
            GUIStyle style = new GUIStyle();
            style.fontStyle = FontStyle.Bold;
            EditorGUILayout.LabelField("OnStart", style);
        }

        if (m_start_INT)
        {
            EditorGUILayout.PropertyField(onStartEvents_INT, label_onStartEvents_INT);
            if (GUILayout.Button("Remove"))
            {
                m_start_INT = false;
                script.ClearOnStartEvents_INT();
            }
        }

        if (m_start_FLOAT)
        {
            EditorGUILayout.PropertyField(onStartEvents_FLOAT, label_onStartEvents_FLOAT);
            if (GUILayout.Button("Remove"))
            {
                m_start_FLOAT = false;
                script.ClearOnStartEvents_FLOAT();
            }
        }

        if (m_start_STRING)
        {
            EditorGUILayout.PropertyField(onStartEvents_STRING, label_onStartEvents_STRING);
            if (GUILayout.Button("Remove"))
            {
                m_start_STRING = false;
                script.ClearOnStartEvents_STRING();
            }
        }
        
        EditorGUILayout.Space();

        if (m_end_INT || m_end_FLOAT || m_end_STRING)
        {
            GUIStyle style = new GUIStyle();
            style.fontStyle = FontStyle.Bold;
            EditorGUILayout.LabelField("OnEnd", style);
        }

        if (m_end_INT)
        {
            EditorGUILayout.PropertyField(onEndEvents_INT, label_onEndEvents_INT);
            if (GUILayout.Button("Remove"))
            {
                m_end_INT = false;
                script.ClearOnEndEvents_INT();
            }
        }

        if (m_end_FLOAT)
        {
            EditorGUILayout.PropertyField(onEndEvents_FLOAT, label_onEndEvents_FLOAT);
            if (GUILayout.Button("Remove"))
            {
                m_end_FLOAT = false;
                script.ClearOnEndEvents_FLOAT();
            }
        }

        if (m_end_STRING)
        {
            EditorGUILayout.PropertyField(onEndEvents_STRING, label_onEndEvents_STRING);
            if (GUILayout.Button("Remove"))
            {
                m_end_STRING = false;
                script.ClearOnEndEvents_STRING();
            }
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
