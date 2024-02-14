using UnityEngine;

public abstract class Part : MonoBehaviour
{
    public virtual void Snap(Transform socketTransform) { print("Snapped abstract"); }
}

