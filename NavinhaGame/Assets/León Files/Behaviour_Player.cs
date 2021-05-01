using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Player : MonoBehaviour
{

    public float speed;
    public float xMax, xMin, yMax, yMin;
    private Rigidbody2D rbody;
    public Transform spawner;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void ShootBullet()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Instantiate
            GameObject bullet = BulletPoolSystem.objInstance.GetObjFromPool();
            if( bullet != null)
            {
                bullet.transform.position = spawner.transform.position;
                bullet.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2 (moveX,moveY).normalized;

        rbody.velocity = movement * speed;

        rbody.position = new Vector2(Mathf.Clamp(rbody.position.x, xMin, xMax), Mathf.Clamp(rbody.position.y, yMin, yMax));

        ShootBullet();
    }
}
