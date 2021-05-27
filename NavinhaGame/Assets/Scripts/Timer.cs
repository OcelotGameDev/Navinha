using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject objRef;
    public float currentTime, startTime;

    private void OnEnable()
    {
        currentTime = startTime;
    }

    void AutoEnableBoss()
    {
        currentTime -= 1 * Time.deltaTime;
        if (currentTime <=0)
        {
            objRef.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AutoEnableBoss();
    }
}
