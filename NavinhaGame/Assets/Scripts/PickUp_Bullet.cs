using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Bullet : PickUps
{
    public override void PickUp(Behaviour_Player player)
    {
        if (!player.companionObj.activeInHierarchy)
        {
            player.companionObj.SetActive(true);
        }
    }
}