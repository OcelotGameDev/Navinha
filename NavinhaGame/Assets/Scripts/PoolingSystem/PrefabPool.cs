using UnityEngine;

[System.Serializable]
public struct PrefabPool
{
    public GameObject Prefab;
    public string Tag;
    public int PrewarmQuantity;
    public bool Prewarm;
}