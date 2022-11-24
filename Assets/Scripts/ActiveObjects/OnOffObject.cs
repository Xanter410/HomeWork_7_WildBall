using UnityEngine;

public class OnOffObject : Interactable
{
    [SerializeField] private bool _setActive = false;

    private void Start()
    {
        gameObject.SetActive(_setActive);
    }

    public override void Activate(PlayerController player)
    {
        SwitchState();
    }

    private void SwitchState()
    {
        _setActive = _setActive ? false : true;

        gameObject.SetActive(_setActive);
    }
}
