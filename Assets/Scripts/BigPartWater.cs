using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BigPartWater : Part
{

    [SerializeField]
    private float cycleDuration = 5.0f;

    Sequence behaviourSequence;
    float SnapDuration = 0.5f;

    Vector2 screenMin;
    Vector2 screenMax;
    float screenBuffer = 1.5f;

    public float MovementSpeed = 1;

    public float RotationDrag = 3;

    Coroutine movement;

    private void Start()
    {
        screenMin = Camera.main.ViewportToWorldPoint(Vector3.zero);
        screenMax = Camera.main.ViewportToWorldPoint(Vector3.one);
        StartBehaviour();
    }

    public override void StartBehaviour()
    {
        movement = StartCoroutine(FishyMovement());
    }

    private IEnumerator FishyMovement()
    {
        while (true)
        {
            transform.Rotate(Vector3.right, Mathf.Sin(Time.time * RotationDrag));
            transform.position += transform.forward.normalized * MovementSpeed * Time.deltaTime;
            yield return null;
        }
    }

    public override void Snap(Transform socketTransform)
    {
        StopCoroutine(movement);
        transform.parent = null;
        transform.parent = socketTransform;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(Vector3.zero, SnapDuration))
                .Join(transform.DORotateQuaternion(socketTransform.rotation, SnapDuration)).OnComplete(TryInvokeSnappedEvent);
    }
}
