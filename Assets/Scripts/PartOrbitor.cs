using DG.Tweening;
using UnityEngine;

public class PartOrbitor : Part
{
    [SerializeField] private float orbitTime = 50.0f;

    [SerializeField]
    Transform plugTransform;

    Vector3 plugOffset;

    Tweener Orbit;
    float SnapDuration = 0.5f;

    void Start()
    {
        Orbit = transform.DOBlendableRotateBy(-Vector3.up * 180.0f, orbitTime).SetLoops(-1, LoopType.Incremental).SetRelative().SetEase(Ease.Linear);
        plugOffset = plugTransform.position - transform.position;
        print("Start");
    }

    public override void Snap(Transform socketTransform)
    {
        print("snapped");
        Orbit.Kill();
        plugTransform.parent = null;
        plugTransform.parent = socketTransform;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(plugTransform.DOLocalMove(Vector3.zero, SnapDuration))
                .Join(plugTransform.DORotateQuaternion(socketTransform.rotation, SnapDuration))
                .AppendCallback(() => Destroy(gameObject));
    }
}