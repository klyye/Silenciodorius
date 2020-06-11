using UnityEditor;
using UnityEngine;

/// <summary>
///     The editor script that allows level generation in the Inspector.
/// </summary>
[CustomEditor(typeof(LevelGenerator))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        var levelGen = target as LevelGenerator;
        if (DrawDefaultInspector() || GUILayout.Button("Generate Level")) levelGen.GenerateLevel();
    }
}