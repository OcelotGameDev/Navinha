using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Armor : PickUps
{
    public override void PickUp(Behaviour_Player player)
    {
        player.companion += 1;
    }
}