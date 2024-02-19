using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPart_Earth : Part
{
    [SerializeField]
    Transform plugTransform;

    LTDescr moveTween;
    LTDescr rotateTween;

    float SnapDuration = 0.5f;

    float screenMin;
    float screenMax;
    float screenBuffer = 1.5f;

    [SerializeField] private Vector3 targetRotation = new Vector3(45, 90, 0);

    private void Start() {
        StartBehaviour();
    }

    public override void StartBehaviour() {
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

        LeanTween.rotate(gameObject, targetRotation, 3)
            .setEaseInOutSine();

        moveTween = LeanTween.value(0, 1, 3)
            .setOnUpdate((float t) => {
                var newX = initialPosition.x + (Mathf.Sin(t * (Mathf.PI * 2)) * radius * modifier);
                var newY = initialPosition.y + (Mathf.Cos(t * (Mathf.PI * 2)) * radius * modifier);
                transform.localPosition = new Vector3(newX, newY, initialPosition.z);
            })
            .setLoopCount(0);

        rotateTween = RotateTween();
    }

    private LTDescr RotateTween() {
        return LeanTween.rotateAroundLocal(gameObject, Vector3.right, 90, 0.5f)
            .setDelay(3.5f)
            .setEaseInOutSine()
            .setOnComplete(() => {
                targetRotation += Vector3.right * 90;
                rotateTween = RotateTween();
            });
    }

    public override void Snap(Transform socketTransform) {
        LeanTween.cancel(moveTween.uniqueId);
        LeanTween.cancel(rotateTween.uniqueId);
        transform.localRotation = Quaternion.Euler(targetRotation);

        plugTransform.parent = null;
        plugTransform.parent = socketTransform;
        plugTransform.localPosition = Vector3.zero;
        plugTransform.rotation = socketTransform.rotation;

        TryInvokeSnappedEvent();
    }
}
