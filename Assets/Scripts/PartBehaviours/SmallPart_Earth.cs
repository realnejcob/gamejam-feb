using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SmallPart_Earth : Part
{
    [SerializeField]
    Transform plugTransform;

    LTDescr moveTween;
    LTDescr rotateTween;

    float SnapDuration = 0.5f;

    float screenMin;
    float screenMax;
    float screenBuffer = 1.5f;

    private void Start()
    {
        StartBehaviour();
    }

    public override void StartBehaviour()
    {
        screenMin = Camera.main.ViewportToWorldPoint(Vector3.zero).x - screenBuffer;
        screenMax = Camera.main.ViewportToWorldPoint(Vector3.one).x + screenBuffer;

        var radius = 0.5f;
        var initialPosition = transform.localPosition;
        var modifier = 0f;

        LeanTween.value(0, 1, 3)
            .setOnUpdate((float val) => {
                modifier = val;
            })
            .setEaseInOutSine();

        LeanTween.rotate(gameObject, new Vector3(45, 90, 0), 3)
            .setEaseInOutSine();

        moveTween = LeanTween.value(0, 1, 3)
            .setOnUpdate((float t) => {
                var newX = initialPosition.x + (Mathf.Sin(t * (Mathf.PI * 2)) * radius * modifier);
                var newY = initialPosition.y +(Mathf.Cos(t * (Mathf.PI * 2)) * radius * modifier);
                transform.localPosition = new Vector3(newX, newY, initialPosition.z);
            })
            .setLoopCount(0);
    }


    public override void Snap(Transform socketTransform) {
        LeanTween.cancel(moveTween.uniqueId);

        plugTransform.parent = null;
        plugTransform.parent = socketTransform;
        plugTransform.localPosition = Vector3.zero;
        plugTransform.rotation = socketTransform.rotation;

        TryInvokeSnappedEvent();
    }
}
