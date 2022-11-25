using System;
using UnityEngine;

public class LevelExitDoor : Interactable
{
    Action _cbWin;

    public override void Activate(PlayerController player)
    {
        LevelFinish();
    }

    public void LevelFinish()
    {
        if (_cbWin == null)
            return;
        _cbWin();
    }

    public void RegisterWinCallback(Action callback)
    {
        _cbWin += callback;
    }
    public void UnRegisterWinCallback(Action callback)
    {
        _cbWin -= callback;
    }
}
