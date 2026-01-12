using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Content/Save File")]
public class SaveFile : ScriptableObject
{
    [Header("Flags")]
    public List<BoolEntry> flags = new();

    [Header("Integers")]
    public List<IntEntry> ints = new();

#if UNITY_EDITOR
    private const string JSON_PATH = "Assets/Content/SaveFile/SaveFile.json";

    [ContextMenu("Apply To JSON")]
    public void ApplyToJson()
    {
        string json = BuildJson(flags, ints);
        File.WriteAllText(JSON_PATH, json);
        AssetDatabase.Refresh();
    }

    private static string BuildJson(List<BoolEntry> bools, List<IntEntry> integers)
    {
        var sb = new StringBuilder();
        sb.Append("{\n  \"flags\": ");
        AppendBoolMap(sb, bools, 2);
        sb.Append(",\n  \"ints\": ");
        AppendIntMap(sb, integers, 2);
        sb.Append("\n}");
        return sb.ToString();
    }

    private static void AppendBoolMap(StringBuilder sb, List<BoolEntry> list, int indent)
    {
        sb.Append("{");
        var entries = CollectEntries(list);
        if (entries.Count > 0)
        {
            sb.Append("\n");
            for (int i = 0; i < entries.Count; i++)
            {
                BoolEntry entry = entries[i];
                AppendIndent(sb, indent + 2);
                sb.Append("\"").Append(EscapeJsonString(entry.key ?? string.Empty)).Append("\": ");
                sb.Append(entry.value ? "true" : "false");
                if (i < entries.Count - 1)
                {
                    sb.Append(",");
                }
                sb.Append("\n");
            }
            AppendIndent(sb, indent);
        }
        sb.Append("}");
    }

    private static void AppendIntMap(StringBuilder sb, List<IntEntry> list, int indent)
    {
        sb.Append("{");
        var entries = CollectEntries(list);
        if (entries.Count > 0)
        {
            sb.Append("\n");
            for (int i = 0; i < entries.Count; i++)
            {
                IntEntry entry = entries[i];
                AppendIndent(sb, indent + 2);
                sb.Append("\"").Append(EscapeJsonString(entry.key ?? string.Empty)).Append("\": ");
                sb.Append(entry.value);
                if (i < entries.Count - 1)
                {
                    sb.Append(",");
                }
                sb.Append("\n");
            }
            AppendIndent(sb, indent);
        }
        sb.Append("}");
    }

    private static List<BoolEntry> CollectEntries(List<BoolEntry> list)
    {
        List<BoolEntry> entries = new();
        if (list == null)
        {
            return entries;
        }

        for (int i = 0; i < list.Count; i++)
        {
            BoolEntry entry = list[i];
            if (entry != null)
            {
                entries.Add(entry);
            }
        }

        return entries;
    }

    private static List<IntEntry> CollectEntries(List<IntEntry> list)
    {
        List<IntEntry> entries = new();
        if (list == null)
        {
            return entries;
        }

        for (int i = 0; i < list.Count; i++)
        {
            IntEntry entry = list[i];
            if (entry != null)
            {
                entries.Add(entry);
            }
        }

        return entries;
    }

    private static void AppendIndent(StringBuilder sb, int count)
    {
        sb.Append(' ', count);
    }

    private static string EscapeJsonString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
#endif
}

[Serializable]
public class BoolEntry
{
    public string key;
    public bool value;
}

[Serializable]
public class IntEntry
{
    public string key;
    public int value;
}

#if UNITY_EDITOR
[CustomEditor(typeof(SaveFile))]
public class SaveFileEditor : Editor
{
    private SerializedProperty flagsProp;
    private SerializedProperty intsProp;

    private void OnEnable()
    {
        flagsProp = serializedObject.FindProperty("flags");
        intsProp = serializedObject.FindProperty("ints");
    }

    public override void OnInspectorGUI()
    {
        if (flagsProp == null || intsProp == null)
        {
            return;
        }

        serializedObject.Update();
        EditorGUILayout.PropertyField(flagsProp, new GUIContent("Flags"), true);
        EditorGUILayout.PropertyField(intsProp, new GUIContent("Integers"), true);

        EditorGUILayout.Space();
        if (GUILayout.Button("Apply To JSON"))
        {
            ((SaveFile)target).ApplyToJson();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
