using System;
using System.Collections;
using UnityEngine;

public class BigEnemyGun : MonoBehaviour
{
    public string currentBullet;
    public BossBullets refe;
    Transform trfr;
    public float cadenceTime;
    private bool _canShoot = false;

    void Start()
    {
        trfr = GetComponent<Transform>();
        refe.gun = trfr.transform;
        StartCoroutine(Cadence());
    }

    private void OnEnable()
    {
        _canShoot = false;
    }

    public void EnableShooting()
    {
        _canShoot = true;
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
            bullet.transform.rotation = trfr.transform.rotation;
            bullet.SetActive(true);
        }

    }

    private IEnumerator Cadence()
    {
        while (true)
        {
            yield return new WaitForSeconds(cadenceTime);
            if (!_canShoot) continue;

            Spawner();
            if (!this.gameObject.activeInHierarchy) yield break;
        }
    }
}
