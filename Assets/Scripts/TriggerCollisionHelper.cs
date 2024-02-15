using UnityEngine;

public class TriggerCollisionHelper : MonoBehaviour
{
    bool isTriggered = false;

    public bool IsTriggered { get { return isTriggered; } }

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        print(gameObject.name + "is true");
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
        print(gameObject.name + "is false");

    }
}
