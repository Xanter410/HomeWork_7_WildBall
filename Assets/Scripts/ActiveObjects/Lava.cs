using UnityEngine;

public class Lava : Interactable
{
    public override void Activate(PlayerController player)
    {
        Debug.Log("�� ������ � ����");
        player.PlayerKill();
    }
}