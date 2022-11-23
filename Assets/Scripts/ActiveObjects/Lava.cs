using UnityEngine;

public class Lava : Interactable
{
    public override void Activate(PlayerController player)
    {
        Debug.Log("Вы попали в лаву");
        player.PlayerKill();
    }
}