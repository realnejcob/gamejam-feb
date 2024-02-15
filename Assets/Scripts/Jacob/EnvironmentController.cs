using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {
    [SerializeField] private float rotationTime = 0.5f;
    [SerializeField] private AnimationCurve rotationCurve;

    private bool canTween = true;

    private void Update() {
        CheckForRotation();
    }

    private void CheckForRotation() {
        if (!canTween)
            return;

        if (Input.GetKeyDown(KeyCode.A)) {
            DoRotation(Vector3.up);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            DoRotation(Vector3.down);
        }
    }

    private void DoRotation(Vector3 _axis) {
        canTween = false;
        var _currentRotation = gameObject.transform.rotation.eulerAngles;
        LeanTween.rotateAround(gameObject, _axis, 90, rotationTime)
            .setEase(rotationCurve)
            .setOnComplete(() => canTween = true);
    }
}
