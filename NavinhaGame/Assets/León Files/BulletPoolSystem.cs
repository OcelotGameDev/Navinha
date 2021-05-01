using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolSystem : MonoBehaviour
{
    public static BulletPoolSystem objInstance;

    public GameObject  bulletPrefab;
    List<GameObject> listOfObjs = new List<GameObject>();
    public int poolsize;
    

    private void Awake()
    {
        if(objInstance = null)
        {
            objInstance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < poolsize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            listOfObjs.Add(obj);
        }
    }

    public GameObject GetObjFromPool()
    {
        for (int i = 0; i < listOfObjs.Count; i++)
        {
            if (!listOfObjs[1].activeInHierarchy)
            {
                return listOfObjs[i];
            }
        }
        return null;
    }

}
