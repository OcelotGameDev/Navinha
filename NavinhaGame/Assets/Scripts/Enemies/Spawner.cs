using System;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
}

#if UNITY_EDITOR

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    private void OnSceneGUI()
    {
        Spawner spawner = target as Spawner;
        
        EditorGUI.BeginChangeCheck();
        
        var transform = spawner.transform;
        var newPosition = Handles.PositionHandle(transform.position, transform.rotation);

        if (EditorGUI.EndChangeCheck())
        {
            
        }
    }
}

#endif // UNITY_EDITOR