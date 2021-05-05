using UnityEngine;

public class Behaviour_Player : MonoBehaviour, IHittable
{
    public float speed;
    public float xMax, xMin, yMax, yMin;
    private Rigidbody2D rbody;
    public Transform gun;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Spawner()
    {
        //Instantiate
        GameObject bullet = PoolingSystem.Instance.SpawnObject("PlayerBullet");

        if (bullet != null)
        {
            bullet.transform.position = gun.position;
            bullet.SetActive(true);
        }
    }

    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Spawner();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveX, moveY).normalized;

        rbody.velocity = movement * speed;

        rbody.position = new Vector2(Mathf.Clamp(rbody.position.x, xMin, xMax), Mathf.Clamp(rbody.position.y, yMin, yMax));

        ShootBullet();
    }
    
    public void Hit(int damage = 1)
    {
    }
}
