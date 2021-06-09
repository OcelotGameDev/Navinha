using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Boss")]
    public GameObject bossRef;
    public float bossStartTime;
    public float bossCurrentTime;

    [Header("PickUps")]
    public GameObject pUpHealth;
    public GameObject pUpComp;

    public float pUpHpStartTime;
    public float pUpHpCurrentTime;
    
    public float pUpCompStartTime;
    public float pUpCompCurrentTime;

    private void OnEnable()
    {
        bossCurrentTime = bossStartTime;
        pUpHpCurrentTime = pUpHpStartTime;
        pUpCompCurrentTime = pUpCompStartTime;
    }

    void AutoEnableBoss()
    {
        bossCurrentTime -= 1 * Time.deltaTime;
        if (bossCurrentTime <= 0)
        {
            bossRef.SetActive(true);
        }
    }

    void AutoEnableHealthPickUp()
    {
        if (!pUpHealth.activeInHierarchy)
        {
            pUpHpCurrentTime -= 1 * Time.deltaTime;
            if (pUpHpCurrentTime <= 0)
            {
                pUpHealth.SetActive(true);
                pUpHpCurrentTime = pUpHpStartTime;
            }
        }
    }

    void AutoEnableCompanioPowerUp()
    {
        if (!pUpComp.activeInHierarchy)
        {
            pUpCompCurrentTime -= 1 * Time.deltaTime;
            if (pUpCompCurrentTime <= 0)
            {
                pUpComp.SetActive(true);
                pUpCompCurrentTime = pUpCompStartTime;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        AutoEnableBoss();
        AutoEnableCompanioPowerUp();
        AutoEnableHealthPickUp();
    }
}
