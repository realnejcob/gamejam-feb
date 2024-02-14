using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedSwish : MonoBehaviour {

    [SerializeField] private Vector3 targetScale = new Vector3(3, 3, 3);
    [SerializeField] private AnimationCurve animationCurve;

    [SerializeField] private float maxTime = 2f;
    [SerializeField] private float minTime = 1f;

    private float spawnTime;

    private Vector3 initPosition;

    private Vector3 currentTarget;
    private float heightOffset = 3;
    private float widthOffset = 4f;

    public void SetPosition(Vector3 _newPosition) {
        transform.position = _newPosition;
    }

    public void InitializeObject() {
        spawnTime = UnityEngine.Random.Range(minTime, maxTime);
        initPosition = transform.position;
        transform.rotation = UnityEngine.Random.rotation;

        currentTarget.z = -0.5f;

        if (initPosition.x > 0) {
            currentTarget += Vector3.right * widthOffset;
        } else {
            currentTarget += Vector3.right * -widthOffset;
        }

        if (initPosition.y > 0) {
            currentTarget += Vector3.up * heightOffset;
        } else {
            currentTarget += Vector3.up * -0.5f;
        }
        transform.localScale = Vector3.zero;
    }

    public void DoSpawnAnimation() {
        LeanTween.value(gameObject, 0, 1, spawnTime)
            .setOnUpdate((float t) => {
                transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, animationCurve.Evaluate(t));
                transform.position = Vector3.Lerp(initPosition, currentTarget, animationCurve.Evaluate(t));
            })
            .setOnComplete(()=>Destroy(gameObject));
    }
}
