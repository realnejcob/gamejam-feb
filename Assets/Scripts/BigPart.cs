using DG.Tweening;
using UnityEngine;

public class BigPart : Part
{
    private float orbitTime = 5.0f;

    Sequence Orbit;
    float SnapDuration = 0.5f;

    float screenMin;
    float screenMax;
    float screenBuffer = 1.5f;
    bool isActivated = true;

    private void Start()
    {
        if (isActivated)
        {
            StartBehaviour();
        }
    }

    public void Deactivate()
    {
        isActivated = false;
    }

    public override void StartBehaviour()
    {
        screenMin = Camera.main.ViewportToWorldPoint(Vector3.zero).x - screenBuffer;
        screenMax = Camera.main.ViewportToWorldPoint(Vector3.one).x + screenBuffer;

        transform.position = new Vector3(screenMax + screenBuffer, transform.position.y, transform.position.z);

        Orbit = DOTween.Sequence();
        Orbit.Append(transform.DOMoveX(screenMin, orbitTime).OnComplete(() => transform.Rotate(0.0f, 180.0f, 0)))
                .Append(transform.DOMoveX(screenMax, orbitTime).OnComplete(() => transform.Rotate(0.0f, 180.0f, 0))).SetLoops(-1);
    }

    public override void Snap(Transform socketTransform)
    {
        Orbit.Kill();
        transform.parent = null;
        transform.parent = socketTransform;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(Vector3.zero, SnapDuration))
                .Join(transform.DORotateQuaternion(socketTransform.rotation, SnapDuration)).OnComplete(TryInvokeSnappedEvent);
    }
}
