using UnityEngine;

public class Behaviour_Player : MonoBehaviour, IHittable
{
    public int maxHp, currentHp, companion;
    public BulletType bulletindex;
    public string currentBullet = "PlayerBullet";
    public float speed, xMax, xMin, yMax, yMin;
    
    //Floats da cadencia de tiro
    public float shootCadence;
    float timer;
    private Rigidbody2D rbody;
    
    private bool IsDead => currentHp <= 0;
    public Transform gun;
    
    [SerializeField] FMOD.Studio.EventInstance fmodInstance;
    
    public GameObject comp;
    void Start()
    {
        fmodInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Game_Sounds/Shot");
        rbody = GetComponent<Rigidbody2D>();
    }
    
    void OnEnable()
    {
        currentHp = maxHp;
        timer = 0f;
    }
    
    public void ChangeBullet(BulletType type)
    {
        switch (type)
        {
            case BulletType.bullet1:
                currentBullet = "";
                break;
            case BulletType.bullet2:
                currentBullet = "";
                break;
            case BulletType.bullet3:
                currentBullet = "";
                break;
            default:
                break;
        }
    }    

    void Spawner()
    {
        //Instantiate
        GameObject bullet = PoolingSystem.Instance.SpawnObject(currentBullet);

        if (bullet != null)
        {
            bullet.transform.position = gun.position;
            bullet.SetActive(true);
        }
    }

    void CompanionOnOff()
    {
        if (companion >= 1)
        {
            comp.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<PickUps>()?.PickUp(this);
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

    void ShootBullet()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
       
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKey(KeyCode.Return))
        { 
            timer = shootCadence;
            Spawner();
            fmodInstance.start();
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
}
