using TMPro;
using UnityEngine;

public class TipsDisplay : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _advicePanel;
    [SerializeField] private TextMeshProUGUI _adviceText;
    private int _activeInteractableObjCount = 0;
    private string _activeInteractableText;

    void Start()
    {
        _activeInteractableText = "Для взаимодействия нажмите E";
        
    }

    private void OnEnable()
    {
        _player.RegisterInteractableObjectsAddorRemCallback(ShowAdviceInteractable);
    }

    private void OnDisable()
    {
        _player.UnRegisterInteractableObjectsAddorRemCallback(ShowAdviceInteractable);
    }

    private void ShowAdviceInteractable(int addOrRemove)
    {
        _activeInteractableObjCount += addOrRemove;
        if (_activeInteractableObjCount > 0)
        {
            _advicePanel.SetActive(true);
            _adviceText.text = _activeInteractableText;
        }
        else
        {
            _advicePanel.SetActive(false);
        }
    }

}
