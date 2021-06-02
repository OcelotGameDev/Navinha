using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Companion : MonoBehaviour, IHittable
{
    Transform trfr;
    public string currentBullet;
    public float cadenceTime, currentHp, maxHp;
    [Range(-1.0f, 1.0f)] public float offsetPosX, offsetPosY;
    private bool IsDead => currentHp <= 0;

    void Start()
    {
        trfr = GetComponent<Transform>();
        StartCoroutine(Cadence());
    }

    void OnEnable()
    {
        currentHp = maxHp;
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

    public void Hit(int damage = 1)
    {
        currentHp -= damage;

        if (IsDead) Die();
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }
}
