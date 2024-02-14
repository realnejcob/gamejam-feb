using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwishSpawner : MonoBehaviour {

    [SerializeField] private Vector3 targetScale = new Vector3(3, 3, 3);
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float spawnTime = 1.5f;

    private Vector3 initPosition;

    private Vector3 currentTarget;
    private Vector3 rightTarget = new Vector3(3.5f, 0, -1);
    private Vector3 leftTarget = new Vector3(-3.5f, 0, -1);

    private void Awake() {
        InitializeObject();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            DoSpawn();
        }
    }

    private void InitializeObject() {
        initPosition = transform.position;
        if (initPosition.x > 0) {
            currentTarget = rightTarget;
        } else {
            currentTarget = leftTarget;
        }

        transform.localScale = Vector3.zero;
    }

    private void DoSpawn() {
        LeanTween.value(gameObject, 0, 1, spawnTime)
            .setOnUpdate((float t) => {
                transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, animationCurve.Evaluate(t));
                transform.position = Vector3.Lerp(initPosition, currentTarget, animationCurve.Evaluate(t));
            });
    }
}
