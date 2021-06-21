using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pool
{
    private readonly GameObject _prefab;
    private Queue<GameObject> _pool;
        
    public Pool(GameObject prefab, bool prewarm = false, int prewarmQuantity = 0)
    {
        _prefab = prefab;
            
        CreatePool();
            
        if (prewarm) 
            Prewarm(prewarmQuantity);
    }

    private void CreatePool()
    {
        _pool = new Queue<GameObject>();
    }

    private GameObject CreateObject()
    {
        GameObject newObj = Object.Instantiate(_prefab);
        Object.DontDestroyOnLoad(newObj);
        return newObj;
    }

    public GameObject GetObject()
    {
        GameObject obj = !_pool.Peek().activeInHierarchy ? _pool.Dequeue() : CreateObject();

        obj.SetActive(true);
        _pool.Enqueue(obj);

        return obj;
    }

    private void Prewarm(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            var obj = CreateObject();
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public void DespawnEveryone()
    {
        foreach (var obj in _pool.Where(o => o.activeInHierarchy))
        {
            obj.SetActive(false);
        }
    }
}