using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private int _totalCountScenes;
    private void Start()
    {
        _totalCountScenes = SceneManager.sceneCountInBuildSettings;
    }
    public void ChoiceLevel(int levelNumber)
    {
        if (levelNumber <= _totalCountScenes)
        {
            SceneManager.LoadScene(levelNumber);
        }
    }
}
