using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    bullet1,
    bullet2,
    bullet3,
}

[RequireComponent (typeof (Rigidbody2D))]
public abstract class PickUps : MonoBehaviour
{
    public abstract void PickUp(Behaviour_Player player);
}