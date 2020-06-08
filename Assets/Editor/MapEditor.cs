using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var mapGen = target as MapGenerator;
        if (DrawDefaultInspector() || GUILayout.Button("Generate Map"))
        {
            mapGen.GenerateMap();
        }
    }
}
