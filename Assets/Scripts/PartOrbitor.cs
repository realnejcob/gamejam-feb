using DG.Tweening;
using UnityEngine;

public class PartOrbitor : Part
{
    [SerializeField]
    Transform plugTransform;

    Vector3 plugOffset;

    Tweener Orbit;
    float SnapDuration = 0.5f;

    void Start()
    {
        Orbit = transform.DOBlendableRotateBy(-Vector3.up * 180.0f, 50.0f).SetLoops(-1, LoopType.Incremental).SetRelative().SetEase(Ease.Linear);
        plugOffset = plugTransform.position - transform.position;
        print("Start");
    }

    public override void Snap(Transform socketTransform)
    {
        print("snapped");
        Orbit.Kill();
        plugTransform.parent = null;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(plugTransform.DOMove(socketTransform.position, SnapDuration))
                .Join(plugTransform.DORotateQuaternion(socketTransform.rotation, SnapDuration))
                .OnComplete(() => plugTransform.parent = socketTransform);
    }
}