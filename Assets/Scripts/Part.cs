using UnityEngine;

public abstract class Part : MonoBehaviour
{

    public PartType partType;
    public ElementType elementType;

    public virtual void Snap(Transform socketTransform) { }

    public virtual void StartBehaviour() { }

    public delegate void SnapEventHandler(Part part);

    public event SnapEventHandler SnappedEvent;

    protected virtual void TryInvokeSnappedEvent()
    {
        SnappedEvent?.Invoke(this);
    }
}

public enum ElementType {
    NONE,
    WATER,
    WIND,
    EARTH,
    FIRE
}

public enum PartType {
    NONE,
    SMALL,
    BIG
}