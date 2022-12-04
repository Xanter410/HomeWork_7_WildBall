using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Interactable
{
    public override void Activate(PlayerController player)
    {
        player.PickUpCoin();

        GameObject.Destroy(gameObject);
    }
}
