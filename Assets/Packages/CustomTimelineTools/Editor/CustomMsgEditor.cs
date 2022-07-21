using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Shinn.Timelinie;

[CustomEditor(typeof(CustomMsgEventManager)), CanEditMultipleObjects]
public class CustomMsgEditor : Editor
{
    public enum OPTIONS
    {
        Int,
        Float,
        String,
    }

    public OPTIONS op;

    private CustomMsgEventManager script;

    private SerializedProperty onStartEvents_INT;
    private SerializedProperty onStartEvents_FLOAT;
    private SerializedProperty onStartEvents_STRING;

    private SerializedProperty onEndEvents_INT;
    private SerializedProperty onEndEvents_FLOAT;
    private SerializedProperty onEndEvents_STRING;

    private GUIContent label_onStartEvents_INT;
    private GUIContent label_onStartEvents_FLOAT;
    private GUIContent label_onStartEvents_STRING;

    private bool m_start_INT = false;
    private bool m_start_FLOAT = false;
    private bool m_start_STRING = false;

    void OnEnable()
    {
        script = (CustomMsgEventManager) target;
        onStartEvents_INT = serializedObject.FindProperty("intEvents");
        onStartEvents_FLOAT = serializedObject.FindProperty("floatEvents");
        onStartEvents_STRING = serializedObject.FindProperty("stringEvents");

        label_onStartEvents_INT = new GUIContent("intEvents");
        label_onStartEvents_FLOAT = new GUIContent("floatEvents");
        label_onStartEvents_STRING = new GUIContent("stringEvents");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        m_start_INT = EditorPrefs.GetBool("m_start_INT");
        m_start_FLOAT = EditorPrefs.GetBool("m_start_FLOAT");
        m_start_STRING = EditorPrefs.GetBool("m_start_STRING");
        
        op = (OPTIONS)EditorGUILayout.EnumPopup("Types:", op);
        if (GUILayout.Button("Create UnityEvent"))
        {
            switch (op)
            {
                case OPTIONS.Int:
                    m_start_INT = true;
                    break;
                case OPTIONS.Float:
                    m_start_FLOAT = true;
                    break;
                case OPTIONS.String:
                    m_start_STRING = true;
                    break;
            }
        }
        EditorGUILayout.Space();
        
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


        EditorPrefs.SetBool("m_start_INT", m_start_INT);
        EditorPrefs.SetBool("m_start_FLOAT", m_start_FLOAT);
        EditorPrefs.SetBool("m_start_STRING", m_start_STRING);

        serializedObject.ApplyModifiedProperties();
    }
}
