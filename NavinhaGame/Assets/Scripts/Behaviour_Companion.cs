using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Companion : MonoBehaviour
{
    Transform trfr;
    public GameObject parentObj;
    public string currentBullet;
    public float cadenceTime, angle;
    [SerializeField] FMOD.Studio.EventInstance fmodInstance;
    [Range(-1.0f, 1.0f)] public float offsetPosX, offsetPosY;
    
    void OnEnable()
    {
        StartCoroutine(Cadence());
        
    }
    
    void Start()
    {
        trfr = GetComponent<Transform>();
        fmodInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Game_Sounds/Shot");
    }

    void Spawner()
    {
        //Instantiate
        GameObject bullet = PoolingSystem.Instance.SpawnObject(currentBullet);

        if (bullet != null)
        {
            fmodInstance.start();
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

    void CompanionOffset()
    {
        //companionObj.transform.position = new Vector2(companionObj.transform.position.x, this.transform.position.y*offSetPosY);
        //transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime); abaixo
        this.transform.RotateAround(parentObj.transform.position, Vector3.forward, angle * Time.deltaTime);
        transform.Rotate((Vector3.forward *-angle)*Time.deltaTime);
    }

    private void Update()
    {
        CompanionOffset();
    }
}
