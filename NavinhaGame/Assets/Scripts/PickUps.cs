using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        var heal = col.gameObject.GetComponent<IHealth>();
        heal?.Heal();

        //armor

        // bullet type

    }
}
