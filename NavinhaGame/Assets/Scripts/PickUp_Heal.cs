using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Heal : PickUps
{
    public override void PickUp(Behaviour_Player player)
    {
        player.currentHp += 1;
    }
}
