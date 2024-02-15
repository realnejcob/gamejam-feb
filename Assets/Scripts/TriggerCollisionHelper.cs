using UnityEngine;

public class TriggerCollisionHelper : MonoBehaviour
{
    [SerializeField]
    bool isTriggered = false;

    Collider otherCollider = null;

    public bool IsTriggered { get { return isTriggered; } }
    public Collider GetOtherCollider { get { return otherCollider; } }

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        otherCollider = other;
        //print(gameObject.name + "is true");
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
        otherCollider = null;
        //print(gameObject.name + "is false");
    }

    private void OnDisable()
    {
        isTriggered = false;
        otherCollider = null;
    }
}
