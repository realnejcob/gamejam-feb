using System;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
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

    private LTDescr pingRotationTween = new LTDescr();
    public Action RotateAction;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        CheckAvailableSockets();
    }

    void Update()
    {
        CheckRotation();
        ConfigureDynamicFollow();
    }

    private void ConfigureDynamicFollow()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ReadyRotation();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            UnReadyRotation();
        }
    }

    private void CheckRotation()
    {
        if (!canTween)
            return;

        if (!rotationReady)
            return;

        var dynamicMousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        var dynamicMousePosDelta = dynamicMousePos - staticMousePos;

        if (dynamicMousePosDelta.y > mousePosTriggerOffset)
        {
            //DoRotation(Vector3.right);
        }
        else if (dynamicMousePosDelta.x < -mousePosTriggerOffset)
        {
            DoRotation(Vector3.back);
            RotateAction?.Invoke();
        }
        else if (dynamicMousePosDelta.y < -mousePosTriggerOffset)
        {
            //DoRotation(Vector3.left);
        }
        else if (dynamicMousePosDelta.x > mousePosTriggerOffset)
        {
            DoRotation(Vector3.forward);
            RotateAction?.Invoke();
        }
    }

    private void DoRotation(Vector3 _axis) {
        UnReadyRotation();
        DisableAllSockets();

        canTween = false;

        var _currentRotation = machineGameObject.transform.rotation.eulerAngles;
        LeanTween.rotateAroundLocal(machineGameObject, _axis, 90, rotationTime)
            .setEase(rotationCurve)
            .setOnComplete(() =>
            {
                EndRotation();
                CheckAvailableSockets();
            });

        StartRotateEffect(new Vector3(0, _axis.z * -1, 0));
    }

    private void EndRotation()
    {
        canTween = true;
    }

    private void ReadyRotation()
    {
        rotationReady = true;
        SetFollowMouse(false);
    }

    private void UnReadyRotation()
    {
        rotationReady = false;
        SetFollowMouse(true);
    }

    private void StartRotateEffect(Vector3 _axis)
    {
        var dir = _axis.y;
        var baseRotateSpeed = 1;

        rotateEffect.transform.localScale = Vector3.one;
        var time = rotationTime * 2;

        var mat = rotateEffect.GetComponentInChildren<MeshRenderer>().material;
        mat.SetFloat("_Amount_X", dir * baseRotateSpeed);

        LeanTween.value(0, 1, time)
            .setEase(rotateEffectCurve)
            .setOnUpdate((float t) =>
            {
                var _newAlpha = Mathf.Lerp(0, 1, t);
                mat.SetFloat("_Alpha", _newAlpha);
            });

        LeanTween.scale(rotateEffect, Vector3.one * 1.1f, time)
            .setEase(rotationCurve);
    }

    private void CheckAvailableSockets()
    {
        for (int i = 0; i < sockets.Count; i++)
        {
            var dot = Vector3.Dot(cam.transform.forward, sockets[i].transform.forward);
            var threshold = 0.1f;

            if (dot >= -threshold && dot <= threshold)
            {
                sockets[i].GetComponent<Collider>().enabled = true;
            }
            else
            {
                sockets[i].GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void DisableAllSockets()
    {
        foreach (var socket in sockets)
        {
            socket.GetComponent<Collider>().enabled = false;
        }
    }

    public void SetFollowMouse(bool value) {
        if (value) {
            machineFollow.SetCanFollowDynamic(true);
            staticMousePos = Vector3.zero;
        } else {
            machineFollow.SetCanFollowDynamic(false);
            staticMousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    public void PingRotation(float time, int freq, Vector3 amt) {
        if (LeanTween.isTweening(pingRotationTween.uniqueId))
                LeanTween.cancel(gameObject, pingRotationTween.uniqueId);

        pingRotationTween = LeanTween.value(gameObject, 0, 1, time)
            .setOnUpdate((float t) => {
                var newRot = Vector3.Lerp(GetNoise(freq, amt), Vector3.zero, t);
                transform.localRotation = Quaternion.Euler(newRot);
            })
            .setEaseOutQuint();
    }

    private Vector3 GetNoise(int frequency, Vector3 maximumTranslationShake) {
        var seed = 1000;

        return new Vector3(
            maximumTranslationShake.x * (Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1),
            maximumTranslationShake.y * (Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2 - 1),
            maximumTranslationShake.z * (Mathf.PerlinNoise(seed + 2, Time.time * frequency) * 2 - 1)
        );
    }

}
