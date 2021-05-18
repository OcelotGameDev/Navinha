using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosGun : MonoBehaviour
{
    public string currentBullet;
    public BossBullets refe;
    Transform trfr;
    public float cadenceTime;

    void Start()
    {
        trfr = GetComponent<Transform>();
        refe.gun = trfr.transform;
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
            bullet.transform.position = trfr.transform.position;
            bullet.SetActive(true);
        }

    }

    private IEnumerator Cadence()
    {
        yield return new WaitForSeconds(cadenceTime);
        Spawner();
    }
}
