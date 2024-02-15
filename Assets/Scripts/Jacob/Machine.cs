using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour {
    [SerializeField] private GameObject machineGameObject;
    [SerializeField] private List<GameObject> sockets = new List<GameObject>();

    [SerializeField] private float rotationTime = 0.5f;
    [SerializeField] private AnimationCurve rotationCurve;

    private bool canTween = true;
    private bool rotationReady = false;

    [SerializeField] private MouseFollow machineFollow;

    private Camera cam;
    private Vector3 staticMousePos;
    private float mousePosTriggerOffset = 0.015f;

    [Header("Effects:")]
    [SerializeField] private GameObject rotateEffect;
    [SerializeField] private AnimationCurve rotateEffectCurve;


    private void Awake() {
        cam = Camera.main;
    }

    private void Start() {
        CheckAvailableSockets();
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
            //DoRotation(Vector3.right);
        } else if (dynamicMousePosDelta.x < -mousePosTriggerOffset) {
            DoRotation(Vector3.up);
        } else if (dynamicMousePosDelta.y < -mousePosTriggerOffset) {
            //DoRotation(Vector3.left);
        } else if (dynamicMousePosDelta.x > mousePosTriggerOffset) {
            DoRotation(Vector3.down);
        }
    }

    private void DoRotation(Vector3 _axis) {
        UnReadyRotation();
        DisableAllSockets();

        canTween = false;

        var _currentRotation = machineGameObject.transform.rotation.eulerAngles;
        LeanTween.rotateAround(machineGameObject, _axis, 90, rotationTime)
            .setEase(rotationCurve)
            .setOnComplete(()=> {
                EndRotation();
                CheckAvailableSockets();
            });

        StartRotateEffect(_axis);
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

    private void StartRotateEffect(Vector3 _axis) {
        var dir = _axis.y;
        var baseRotateSpeed = 1.5f;

        rotateEffect.transform.localScale = Vector3.one;
        var time = rotationTime * 1;

        var mat = rotateEffect.GetComponentInChildren<MeshRenderer>().material;
        mat.SetFloat("_Amount_X", dir * baseRotateSpeed);

        LeanTween.value(0,1, time)
            .setEase(rotateEffectCurve)
            .setOnUpdate((float t) => {

                var _newAlpha = Mathf.Lerp(0, 1, t);
                mat.SetFloat("_Alpha", _newAlpha);
        });

        LeanTween.scale(rotateEffect, Vector3.one * 1.1f, time)
            .setEase(rotationCurve);
    }

    private void CheckAvailableSockets() {
        for (int i = 0; i < sockets.Count; i++) {
            var dot = Vector3.Dot(cam.transform.forward, sockets[i].transform.forward);
            var threshold = 0.1f;

            if (dot >= -threshold && dot <= threshold) {
                sockets[i].SetActive(true);
            } else {
                sockets[i].SetActive(false);
            }
        }
    }

    private void DisableAllSockets() {
        foreach (var socket in sockets) {
            socket.SetActive(false);
        }
    }
}
