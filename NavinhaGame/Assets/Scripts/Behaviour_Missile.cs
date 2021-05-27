using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Missile : MonoBehaviour, IHittable
{
    Vector2 dir, nextPos;
    Rigidbody2D rbody;
    public Transform playerT;
    public float lerpSpeed, recalTime, currentHp, maxHp, rotAngle;
    float angle;
    bool IsDead => currentHp <= 0;

    void OnEnable()
    {
        currentHp = maxHp;
    }

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveToPlayerPos());
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

    private IEnumerator MoveToPlayerPos()
    {
        while (true)
        {
            yield return new WaitForSeconds(recalTime);
            nextPos = playerT.position;
            if (!this.gameObject.activeInHierarchy)
            {
                yield break;
            }
        }
    }

    void Update()
    {
        dir = playerT.position - this.transform.position;
        dir = dir.normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.position = Vector2.Lerp(transform.position, nextPos, lerpSpeed * Time.deltaTime);

    }
}
