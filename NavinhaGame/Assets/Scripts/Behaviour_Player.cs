using UnityEngine;

public class Behaviour_Player : MonoBehaviour, IHittable
{
    [Header("Movement Settings")]
    public float speed, xMax, xMin, yMax, yMin;
    private Rigidbody2D rbody;
    Animator anim;
    
    [Header("CompanionSettings")]
    [Range(0.0f, 1.0f)]
    public float offSetPosY;
    public GameObject companionObj;

    [Header("Floats da cadencia de tiro")] 
    public float shootCadence;
    float timer;

    [Header("Life Settings")]
    public int maxHp;
    public int currentHp;
    private bool IsDead => currentHp <= 0;
    
    [Header("Shooting Settings")]
    public BulletType bulletindex;
    public string currentBullet = "PlayerBullet";
    public Transform gun;
    public bool shooting;

    [SerializeField] FMOD.Studio.EventInstance fmodInstance;
    
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        fmodInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Game_Sounds/Shot");
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

    void CompanionOffset()
    {
        companionObj.transform.position = new Vector2(companionObj.transform.position.x, this.transform.position.y*offSetPosY);
    }

    void ShootBullet()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            //shooting = false;
        }

        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKey(KeyCode.Return))
        {
            timer = shootCadence;
            Spawner();
            shooting = true;
            fmodInstance.start();
        }
        else { shooting = false; }
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

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<PickUps>()?.PickUp(this);
    }

    public void Hit(int damage = 1)
    {
        if (companionObj.activeInHierarchy)
        {
            companionObj.SetActive(false);
        }
        else {currentHp -= damage;}
        
        if (IsDead) Die();
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }

    void MoveAnimate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveX, moveY).normalized;

        rbody.position += movement * speed * Time.deltaTime;
        rbody.position = new Vector2(Mathf.Clamp(rbody.position.x, xMin, xMax), Mathf.Clamp(rbody.position.y, yMin, yMax));

        anim.SetBool("shoot",shooting);
        anim.SetFloat("fly", movement.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnimate();
        ShootBullet();
        CompanionOffset();
    }
}
