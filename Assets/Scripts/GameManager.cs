using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private LevelExitDoor _levelExit;
    private int _totalCountScenes;

    private void Start()
    {
        _totalCountScenes = SceneManager.sceneCountInBuildSettings;
    }

    private void OnEnable()
    {
        _player.RegisterPlayerDeadedCallback(GameOver);
        _levelExit.RegisterWinCallback(FinishLevel);
    }

    private void OnDisable()
    {
        _player.UnRegisterPlayerDeadedCallback(GameOver);
        _levelExit.UnRegisterWinCallback(FinishLevel);
    }

    private void GameOver()
    {
        Debug.Log("Игра проиграна");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FinishLevel()
    {
        int currentSceneID = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneID < _totalCountScenes - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("Это был последний уровень! \nВы прошли игру!");
        }
    }
}
