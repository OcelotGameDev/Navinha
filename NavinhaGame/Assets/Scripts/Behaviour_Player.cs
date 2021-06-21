using System;
using UnityEngine;

public class Behaviour_Player : MonoBehaviour, IHittable
{
    [Header("Movement Settings")]
    public float speed, xMax, xMin, yMax, yMin;
    private Rigidbody2D rbody;
    Animator anim;
    
    [Header("CompanionSettings")]
    public GameObject companionObj;

    [Header("Floats da cadencia de tiro")] 
    public float shootCadence;
    float timer;

    [Header("Life Settings")]
    public int maxHp;
    public int currentHp;
    public string explosion;
    private bool IsDead => currentHp <= 0;
    
    [Header("Shooting Settings")]
    public BulletType bulletindex;
    public string currentBullet = "PlayerBullet";
    public Transform gun;
    public bool shooting;

    [SerializeField] FMOD.Studio.EventInstance fmodShot, fmodHit;

    public static Action OnDie;
    
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        fmodShot = FMODUnity.RuntimeManager.CreateInstance("event:/Game_Sounds/Shot (Sem Reverb)");
        fmodHit= FMODUnity.RuntimeManager.CreateInstance("event:/Game_Sounds/Hit Curto");
    }
    
    void OnEnable()
    {
        currentHp = maxHp;
        timer = 0f;
    }
    
    /*public void ChangeBullet(BulletType type)
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
    }*/

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
            shooting = true;
            fmodShot.start();
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

    void SpawnVFX()
    {
        GameObject vfx = PoolingSystem.Instance.SpawnObject(explosion);
        if (vfx != null)
        {
            vfx.transform.position = this.transform.position;
            vfx.SetActive(true);
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
            fmodHit.start();
            companionObj.SetActive(false);
        }
        else { fmodHit.start(); currentHp -= damage;}
        
        if (IsDead) Die();
    }

    private void Die()
    {
        SpawnVFX();
        OnDie?.Invoke();
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
    }
}
