using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    private void OnEnable()
    {
        _player.RegisterPlayerDeadedCallback(GameOver);
    }

    private void OnDisable()
    {
        _player.UnRegisterPlayerDeadedCallback(GameOver);
    }

    private void GameOver()
    {
        Debug.Log("Игра проиграна");
    }

}
