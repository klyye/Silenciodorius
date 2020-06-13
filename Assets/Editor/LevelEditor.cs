using UnityEditor;
using UnityEngine;

/// <summary>
///     The editor script that allows level generation in the Inspector.
/// </summary>
[CustomEditor(typeof(LevelManager))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var levelGen = target as LevelManager;
        if (DrawDefaultInspector() || GUILayout.Button("Generate Level"))
        {
            
            levelGen.InstantiateCurrentLevel();
        }
    }
}