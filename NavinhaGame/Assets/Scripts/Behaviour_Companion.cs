using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Companion : MonoBehaviour
{
    public string currentBullet;
    Transform trfr;
    public float cadenceTime;

    void Start()
    {
        trfr = GetComponent<Transform>();
        StartCoroutine(Cadence());
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }

    void Spawner()
    {
        //Instantiate
        GameObject bullet = PoolingSystem.Instance.SpawnObject(currentBullet);

        if (bullet != null)
        {
            bullet.transform.position = this.gameObject.transform.position;
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
