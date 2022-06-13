using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class AssetHandler
{
    [OnOpenAsset]
    public static bool OpenEditor(int instanceID, int line)
    {
        SO_PopulateTerrains obj = EditorUtility.InstanceIDToObject(instanceID) as SO_PopulateTerrains;
        if(obj != null)
        {
            GenerationDataEditorWindow.Open(obj);
            return true;
        }
        return false;
    }
}

[CustomEditor(typeof(SO_PopulateTerrains))]
public class GenerationDataCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Editor"))
        {
            GenerationDataEditorWindow.Open((SO_PopulateTerrains)target);
        }
    }
}
