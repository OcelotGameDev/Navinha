using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossTwoMove : MonoBehaviour, IHittable
{
    Vector2 currentPos, nextPos;
    Vector2 dir;

    public Transform[] arrayPos;
    public Transform child;
    public float lerpSpeed, cadenceTimeHigh, cadenceTimeLower, currentHp, maxHp;
    public GameObject vfx;

    public float angle;
    int arrayIndex;
    
    Rigidbody2D rbody;
    bool IsDead => currentHp <= 0;

    void OnEnable()
    {
        currentHp = maxHp;
    }

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveToNexPosition());
    }

    public void Hit(int damage = 1)
    {
        currentHp -= damage;
        if (IsDead) Die();
    }

    private void Die()
    {
        Instantiate(vfx, this.transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    void IncrementIndex()
    {
        arrayIndex = Random.Range(0, arrayPos.Length);
        nextPos = arrayPos[arrayIndex].position;
    }

    private IEnumerator MoveToNexPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(cadenceTimeLower,cadenceTimeHigh));
            IncrementIndex();
            //Debug.Log("Position");
            if (!this.gameObject.activeInHierarchy)
            {
                yield break;
            }
        }
    }

    void Update()
    {
        /*dir = playerT.position - this.transform.position;
        dir = dir.normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,angle);*/
        transform.position = Vector2.Lerp(transform.position, nextPos, lerpSpeed * Time.deltaTime);
        child.transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, angle)*Time.deltaTime);
    }
}