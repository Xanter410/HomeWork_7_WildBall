using UnityEngine;

public class TestObject : Interactable
{
    public override void Activate()
    {
        transform.transform.localPosition = Vector3.zero;
    }
}
