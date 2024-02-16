using UnityEngine;

public abstract class Part : MonoBehaviour
{
    public virtual void Snap(Transform socketTransform) { }

    public virtual void StartBehaviour() { }

    public delegate void SnapEventHandler(Part part);

    public event SnapEventHandler SnappedEvent;

    protected virtual void TryInvokeSnappedEvent()
    {
        SnappedEvent?.Invoke(this);
    }
}

