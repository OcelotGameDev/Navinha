using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new Pool List", menuName = "Pool List", order = 1)]
public class PoolPrefabScriptableObject : ScriptableObject
{
    [FormerlySerializedAs("_prefabPools")] public PrefabPool[] PrefabPools;
}
