using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private Vector2 _start;
    
    [SerializeField] private Vector2 _end;

    [SerializeField] private Vector2 _direction;

    public Vector2 Direction { get => _direction; set => _direction = value; }
    public Vector2 End { get => _end; set => _end = value; }
    public Vector2 Start { get => _start; set => _start = value; }

    public Quaternion Rotation => _currentRotation;
    public Vector3 RandomPoint => Vector3.Lerp(_currentStart, _currentEnd, Random.Range(0f, 1f));

    // Auxiliar variables
    private Vector2 _currentStart, _currentEnd;
    private Quaternion _currentRotation;

    private void OnEnable()
    {
        var aux = this.transform;
        _currentStart = aux.TransformPoint(_start);
        _currentEnd = aux.TransformPoint(_end);
        _currentRotation = aux.rotation * Quaternion.Euler(0, 0,
            Vector2.SignedAngle(aux.right, aux.TransformDirection(_direction)));
    }
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.TransformPoint(_start), this.transform.TransformPoint(_end));
        
        Handles.color = Color.magenta;

        int arrowCount = 10;

        for (int i = 0; i <= arrowCount; i++)
        {
            Handles.ArrowHandleCap(0, Vector3.Lerp(this.transform.TransformPoint(_start), this.transform.TransformPoint(_end), (float) (i)/ (float)(arrowCount)), this.transform.rotation * Quaternion.LookRotation(_direction), 1, EventType.Repaint);
        }
        
        Handles.color = Color.white;
    }
#endif
}

#if UNITY_EDITOR

[CustomEditor(typeof(SpawnArea))]
public class SpawnAreaEditor : Editor
{
    private void OnSceneGUI()
    {
        SpawnArea spawnArea = target as SpawnArea;
        
        float size = HandleUtility.GetHandleSize(spawnArea.transform.position) * 0.1f;
        float snap = 0.1f;
        Vector3 handleDirection = Vector3.forward;
        
        EditorGUI.BeginChangeCheck();
        
        Handles.color = Color.green;
        
        var newStartPosition =  spawnArea.transform.InverseTransformPoint(Handles.Slider2D( spawnArea.transform.TransformPoint(spawnArea.Start), handleDirection, Vector3.right, Vector3.up, size, Handles.DotHandleCap, snap));
        var newEndPosition =  spawnArea.transform.InverseTransformPoint(Handles.Slider2D( spawnArea.transform.TransformPoint(spawnArea.End), handleDirection, Vector3.right, Vector3.up, size, Handles.DotHandleCap, snap));
        
        Handles.color = Color.magenta;
        
        var newDirection = spawnArea.transform.InverseTransformPoint(Handles.Slider2D( spawnArea.transform.TransformPoint(spawnArea.Direction), handleDirection, Vector3.right, Vector3.up, size, Handles.DotHandleCap, snap));

        Handles.DrawLine(spawnArea.transform.position, spawnArea.transform.TransformPoint(spawnArea.Direction));
        
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spawnArea, "Change on Spawn Area");

            spawnArea.Start = newStartPosition;
            spawnArea.End = newEndPosition;
            spawnArea.Direction = newDirection;
        }
        
        Handles.color = Color.white;
    }
}

#endif //UNITY_EDITOR