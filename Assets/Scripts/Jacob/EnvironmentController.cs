using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {
    [SerializeField] private int currentEnviromentIndex = 0;
    [SerializeField] private float rotationTime = 0.5f;
    [SerializeField] private AnimationCurve rotationCurve;
    [SerializeField] private float zoomCamSize = 1.7f;
    [SerializeField] private AnimationCurve zoomCurve;

    private bool canTween = true;

    private Camera cam;
    private float initCamSize;

    private void Awake() {
        cam = Camera.main;
        initCamSize = cam.orthographicSize;
    }

    private void Update() {
        CheckForRotation();
    }

    private void CheckForRotation() {
        if (!canTween)
            return;

        if (Input.GetKeyDown(KeyCode.A)) {
            DoRotation(Vector3.up);
            IncrementEnvironmentIndex(-1);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            DoRotation(Vector3.down);
            IncrementEnvironmentIndex(1);
        }
    }

    private void DoRotation(Vector3 _axis) {
        canTween = false;
        var _currentRotation = gameObject.transform.rotation.eulerAngles;
        LeanTween.value(gameObject, 0, 1, rotationTime)
            .setOnUpdate((float t) => {
                cam.orthographicSize = Mathf.Lerp(initCamSize, zoomCamSize, zoomCurve.Evaluate(t));
            });
        LeanTween.rotateAround(gameObject, _axis, 90, rotationTime)
            .setEase(rotationCurve)
            .setOnComplete(() => canTween = true);
    }

    private void IncrementEnvironmentIndex(int amt) {
        currentEnviromentIndex += amt;
        if (currentEnviromentIndex < 0)
            currentEnviromentIndex = 3;
        currentEnviromentIndex = currentEnviromentIndex % 4;
    }
}
