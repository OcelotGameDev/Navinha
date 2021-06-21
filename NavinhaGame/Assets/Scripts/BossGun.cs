using System.Collections;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    public string currentBullet;
    public BossBullets refe;
    Transform trfr, gunT;
    Vector2 rangePosition;
    public float cadenceTime, xMin, xMax, yMin, yMax;
    private bool _canShoot = true;
    
    private Coroutine _cadenceCoroutine; 

    void Awake()
    {
        //rangePosition = new Vector2(Mathf.Clamp(rangePosition.x, xMin, xMax), Mathf.Clamp(rangePosition.y, yMin, yMax));
        gunT = transform;
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

    void ClampShoot()
    {
        if (((gunT.position.x > xMin && gunT.position.x < xMax) && (gunT.position.y > yMin && gunT.position.y < yMax)))
        {
            Spawner();
        }
    }

    private IEnumerator Cadence()
    {
        while (true)
        {
            yield return new WaitForSeconds(cadenceTime);
            if (!_canShoot) continue;
            ClampShoot();
            if (!this.gameObject.activeInHierarchy) yield break;
        }
    }
}