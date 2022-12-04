using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private LevelExitDoor _levelExit;
    [SerializeField] private Canvas _GameEnd;
    [SerializeField] private TextMeshProUGUI _GameEndText;
    private int _totalCountScenes;
    private int _levelScore = 0;
    private static int _totalScore = 0;

    private void Start()
    {
        Time.timeScale = 1;
        _levelScore = 0;
        _totalCountScenes = SceneManager.sceneCountInBuildSettings;
    }

    private void OnEnable()
    {
        _player.RegisterPlayerDeadedCallback(GameOver);
        _player.RegisterPickUpCoinCallback(PickUpCoin);
        _levelExit.RegisterWinCallback(FinishLevel);
    }

    private void OnDisable()
    {
        _player.UnRegisterPlayerDeadedCallback(GameOver);
        _player.UnRegisterPickUpCoinCallback(PickUpCoin);
        _levelExit.UnRegisterWinCallback(FinishLevel);
    }

    private void GameOver()
    {
        Debug.Log("Игра проиграна");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FinishLevel()
    {
        _totalScore =+ _levelScore;
        int currentSceneID = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneID < _totalCountScenes - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            GameEnd();
        }
    }

    private void GameEnd()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;

        _GameEnd.gameObject.SetActive(true);
        _GameEndText.text = $"Вы прошли всю игру!\nОчков набрано: {_totalScore}";
    }

    private void PickUpCoin()
    {
        _levelScore++;
    }
}
