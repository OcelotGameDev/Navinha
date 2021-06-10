using System.Collections;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    public string currentBullet;
    public BossBullets refe;
    Transform trfr;
    public float cadenceTime;
    private bool _canShoot = true;
    
    private Coroutine _cadenceCoroutine; 

    void Awake()
    {
        trfr = GetComponent<Transform>();
        refe.gun = trfr.transform;
    }

    private void OnEnable()
    {
        _cadenceCoroutine = StartCoroutine(Cadence());
    }

    private void OnDisable()
    {
        StopCoroutine(_cadenceCoroutine);
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }

    void Spawner()
    {
        //Instantiate
        GameObject bullet = PoolingSystem.Instance.SpawnObject(currentBullet);

        Debug.Log("Spawn BUllet", this.gameObject);
        if (bullet != null)
        {
            Debug.Log("Bullet Spawned", this.gameObject);
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