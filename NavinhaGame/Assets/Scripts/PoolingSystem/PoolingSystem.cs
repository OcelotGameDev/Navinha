using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PoolingSystem : MonoBehaviour
{
    public static PoolingSystem Instance { get; private set; } = null;
    [SerializeField] private PoolPrefabScriptableObject _poolsSO;
    private Dictionary<string, Pool> _poolsDictionary;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        SetupPools(_poolsSO.PrefabPools);
    }

    private void Start()
    {
        /*var operation = Addressables.LoadAssetAsync<PoolPrefabScriptableObject>("PoolList");
        
        while (!operation.IsDone)
        {
            yield return null;
        }

        var poolList = operation.Result;

        if (poolList != null)
        {
            SetupPools(poolList.PrefabPools);
        }*/
    }
    
    private void SetupPools(PrefabPool[] prefabPools)
    {
        _poolsDictionary = new Dictionary<string, Pool>();

        foreach (var prefab in prefabPools)
        {
            if (_poolsDictionary.ContainsKey(prefab.Tag)) continue;

            var pool = new Pool(prefab.Prefab, prefab.Prewarm, prefab.PrewarmQuantity);
            _poolsDictionary.Add(prefab.Tag, pool);
        }
    }

    public GameObject SpawnObject(string tag)
    {
        if (_poolsDictionary == null) return null;
        if (!_poolsDictionary.ContainsKey(tag)) return null;

        return _poolsDictionary[tag].GetObject();
    }

    public GameObject SpawnObject(string tag, Vector3 position, Quaternion rotation)
    {
        if (_poolsDictionary == null) return null;
        if (!_poolsDictionary.ContainsKey(tag)) return null;

        var obj = _poolsDictionary[tag].GetObject();

        obj.transform.position = position;
        obj.transform.rotation = rotation;

        return obj;
    }

    public GameObject SpawnObject(string tag, Transform trans, bool parent = false)
    {
        var obj = SpawnObject(tag, trans.position, trans.rotation);

        if (parent && obj != null)
        {
            obj.transform.parent = trans;
        }
        
        return obj;
    }
}