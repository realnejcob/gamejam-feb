using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour {
    [SerializeField] private GameObject machineGameObject;

    [SerializeField] private float rotationTime = 0.5f;
    [SerializeField] private AnimationCurve rotationCurve;

    private bool canTween = true;

    void Update() {
        CheckRotation();
    }

    private void CheckRotation() {
        if (!canTween)
            return;

        if (Input.GetKeyDown(KeyCode.W)) {
            DoRotation(Vector3.right);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            DoRotation(Vector3.up);
        } else if (Input.GetKeyDown(KeyCode.S)) {
            DoRotation(Vector3.left);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            DoRotation(Vector3.down);
        }
    }

    private void DoRotation(Vector3 _axis) {
        canTween = false;
        var _currentRotation = machineGameObject.transform.rotation.eulerAngles;
        LeanTween.rotateAround(machineGameObject, _axis, 90, rotationTime)
            .setEase(rotationCurve)
            .setOnComplete(() => canTween = true);
    }
}
