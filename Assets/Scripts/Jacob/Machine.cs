using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour {
    [SerializeField] private GameObject machineGameObject;

    [SerializeField] private float rotationTime = 0.5f;
    [SerializeField] private AnimationCurve rotationCurve;

    private bool canTween = true;
    private bool rotationReady = false;

    [SerializeField] private MouseFollow machineFollow;

    private Camera cam;
    private Vector3 staticMousePos;
    private float mousePosTriggerOffset = 0.015f;

    [Header("Effects:")]
    [SerializeField] private MeshRenderer rotateEffect;
    [SerializeField] private AnimationCurve rotateEffectCurve;


    private void Awake() {
        cam = Camera.main;
    }

    void Update() {
        CheckRotation();
        ConfigureDynamicFollow();
    }

    private void ConfigureDynamicFollow() {
        if (Input.GetMouseButtonDown(1)) {
            ReadyRotation();
        } else if (Input.GetMouseButtonUp(1)) {
            UnReadyRotation();
        }
    }

    private void CheckRotation() {
        if (!canTween)
            return;

        if (!rotationReady)
            return;

        var dynamicMousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        var dynamicMousePosDelta = dynamicMousePos - staticMousePos;

        if (dynamicMousePosDelta.y > mousePosTriggerOffset) {
            DoRotation(Vector3.right);
        } else if (dynamicMousePosDelta.x < -mousePosTriggerOffset) {
            DoRotation(Vector3.up);
        } else if (dynamicMousePosDelta.y < -mousePosTriggerOffset) {
            DoRotation(Vector3.left);
        } else if (dynamicMousePosDelta.x > mousePosTriggerOffset) {
            DoRotation(Vector3.down);
        }
    }

    private void DoRotation(Vector3 _axis) {
        UnReadyRotation();

        canTween = false;
        var _currentRotation = machineGameObject.transform.rotation.eulerAngles;
        LeanTween.rotateAround(machineGameObject, _axis, 90, rotationTime)
            .setEase(rotationCurve)
            .setOnComplete(EndRotation);

        StartRotateEffect();
    }

    private void EndRotation() {
        canTween = true;
    }

    private void ReadyRotation() {
        rotationReady = true;
        machineFollow.SetCanFollowDynamic(false);
        staticMousePos = cam.ScreenToViewportPoint(Input.mousePosition);
    }

    private void UnReadyRotation() {
        rotationReady = false;
        machineFollow.SetCanFollowDynamic(true);
        staticMousePos = Vector3.zero;
    }

    private LTDescr StartRotateEffect() {
        var mat = rotateEffect.material;
        return LeanTween.value(0,1,rotationTime * 0.9f)
            .setEase(rotateEffectCurve)
            .setOnUpdate((float t) => {
                var value = Mathf.Lerp(0, 1, t);
                mat.SetFloat("_Alpha", value);
        });
    }
}
