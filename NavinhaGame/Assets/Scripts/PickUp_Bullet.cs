using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Bullet : PickUps
{
    public BulletType type;

    public override void PickUp(Behaviour_Player player)
    {
        player.bulletindex = type;
    }
}