using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {
    private int currentEnviromentIndex = 0;
    [SerializeField] private Environment currentEnvironment;
    [SerializeField] private List<Environment> environments = new List<Environment>();

    [SerializeField] private float rotationTime = 0.5f;
    [SerializeField] private AnimationCurve rotationCurve;
    [SerializeField] private float zoomCamSize = 1.7f;
    [SerializeField] private AnimationCurve zoomCurve;

    private bool canTween = true;

    private Camera cam;
    private float initCamSize;

    private bool isLookingUp = false;

    private void Awake() {
        cam = Camera.main;
        initCamSize = cam.orthographicSize;
    }

    private void Start() {
        InitializeEnvironments(currentEnvironment);
    }

    private void Update() {
        CheckForRotation();
    }

    private void CheckForRotation() {
        if (!canTween)
            return;

        if (Input.GetKeyDown(KeyCode.A)) {
            if (isLookingUp)
                return;

            IncrementEnvironmentIndex(-1);
            DoRotation(Vector3.up, rotationTime);
            DoZoom();
        } else if (Input.GetKeyDown(KeyCode.D)) {
            if (isLookingUp)
                return;

            IncrementEnvironmentIndex(1);
            DoRotation(Vector3.down, rotationTime);
            DoZoom();
        } else if (Input.GetKeyDown(KeyCode.W)) {
            if (isLookingUp)
                return;

            isLookingUp = true;
            DoRotation(Vector3.right, rotationTime * 1.5f);
        } else if (Input.GetKeyDown(KeyCode.S)) {
            if (!isLookingUp)
                return;

            isLookingUp = false;
            DoRotation(Vector3.left, rotationTime*1.5f);
        }
    }

    private void DoRotation(Vector3 _axis, float _rotationTime) {
        canTween = false;

        SetAllEnvironmentsInactive();

        LeanTween.rotateAround(gameObject, _axis, 90, _rotationTime)
            .setEase(rotationCurve)
            .setOnComplete(() => {
                canTween = true;
                SetCurrentEnvironment(currentEnviromentIndex);
            });
    }

    private void DoZoom() {
        LeanTween.value(gameObject, 0, 1, rotationTime)
        .setOnUpdate((float t) => {
            cam.orthographicSize = Mathf.Lerp(initCamSize, zoomCamSize, zoomCurve.Evaluate(t));
        });
    }

    private void IncrementEnvironmentIndex(int amt) {
        currentEnviromentIndex += amt;
        if (currentEnviromentIndex < 0)
            currentEnviromentIndex = 3;
        currentEnviromentIndex = currentEnviromentIndex % 4;
    }

    private void SetCurrentEnvironment(int idx) {
        currentEnvironment = environments[idx];
        currentEnvironment.SetActive(true);
    }

    private void InitializeEnvironments(Environment _currentEnvironment) {
        for (int i = 0; i < environments.Count; i++) {
            var e = environments[i];
            if (e == _currentEnvironment) {
                e.SetActive(true);
            } else {
                e.SetActive(false);
            }
        }
    }

    private void SetAllEnvironmentsInactive() {
        foreach (var e in environments) {
            e.SetActive(false);
        }
    }

    public bool GetIsLookingUp() {
        return isLookingUp;
    }
}
