using UnityEngine;

public abstract class Part : MonoBehaviour
{
    public virtual void Snap(Transform socketTransform) { }

    public virtual void StartBehaviour() { }
}

