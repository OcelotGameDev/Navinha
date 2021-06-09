using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Companion : MonoBehaviour
{
    Transform trfr;
    public string currentBullet;
    public float cadenceTime;
    [Range(-1.0f, 1.0f)] public float offsetPosX, offsetPosY;
    
    void OnEnable()
    {
        StartCoroutine(Cadence());
    }
    
    void Start()
    {
        trfr = GetComponent<Transform>();
    }

    void Spawner()
    {
        //Instantiate
        GameObject bullet = PoolingSystem.Instance.SpawnObject(currentBullet);

        if (bullet != null)
        {
            bullet.transform.position = new Vector2(trfr.transform.position.x + offsetPosX, trfr.transform.position.y + offsetPosY);
            bullet.SetActive(true);
        }
    }

    private IEnumerator Cadence()
    {
        while (true)
        {
            yield return new WaitForSeconds(cadenceTime);
            Spawner();
            if (!this.gameObject.activeInHierarchy)
            {
                yield break;
            }
        }
    }
}
