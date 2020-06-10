using UnityEditor;
using UnityEngine;

/// <summary>
///     The editor script that allows map generation in the Inspector.
/// </summary>
[CustomEditor(typeof(LevelGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        var mapGen = target as LevelGenerator;
        if (DrawDefaultInspector() || GUILayout.Button("Generate Map")) mapGen.GenerateLevel();
    }
}