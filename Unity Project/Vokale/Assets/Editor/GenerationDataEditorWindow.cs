using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GenerationDataEditorWindow : ExtendedEditorWindow
{
    public static void Open(SO_PopulateTerrains terrainData)
    {
        GenerationDataEditorWindow window = GetWindow<GenerationDataEditorWindow>("Terrain Generation Data Editor");
        window.serializedObject = new SerializedObject(terrainData);
    }

    private void OnGUI()
    {
        currentProperty = serializedObject.FindProperty("terrains");

        DrawAdder(currentProperty);
        DrawRemover(currentProperty);
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(200), GUILayout.ExpandHeight(true));

        DrawSidebar(currentProperty);

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        if(selectedProperty != null)
        {
            DrawProperties(selectedProperty, true);
        }
        else
        {
            EditorGUILayout.LabelField("Select a terrain item from the list");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        Apply();
    }
}
