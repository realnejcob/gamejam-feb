using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float moveAmount = 1;
    [SerializeField] private float rotXSpeed = 1;
    [SerializeField] private float rotYSpeed = 1;
    [SerializeField] private float rotZSpeed = 1;

    private float realMoveSpeed;
    private float realMoveAmount;
    private float realRotXSpeed;
    private float realRotYSpeed;
    private float realRotZSpeed;

    [SerializeField] private Transform currentTarget;
    [SerializeField] private Transform breakableTargetsGroup;
    private Vector3 velocity;

    [Header("Parts:")]
    [SerializeField] private List<PartBreakable> partBreakables = new List<PartBreakable>();
    [SerializeField] private Part partReward;

    [SerializeField] private ParticleSystem breakParticleSystem;

    private Machine machine;

    private void Awake() {
        machine = FindObjectOfType<Machine>();
        machine.BreakAction += TryDetach;
    }

    private void Start() {
        transform.rotation = Random.rotation;

        realMoveSpeed = Random.Range(realMoveSpeed, -realMoveSpeed);
        realMoveAmount = Random.Range(realMoveAmount, -realMoveAmount);
        realRotXSpeed = Random.Range(rotXSpeed, -rotXSpeed);
        realRotYSpeed = Random.Range(rotYSpeed, -rotYSpeed);
        realRotZSpeed = Random.Range(rotZSpeed, -rotZSpeed);
    }

    private void Update() {
        var sineMovement = new Vector3(Mathf.Sin(Time.time * realMoveSpeed) * realMoveAmount, 0, 0);

        transform.Rotate(Vector3.right, realRotXSpeed);
        transform.Rotate(Vector3.up, realRotYSpeed);
        transform.Rotate(Vector3.forward, realRotZSpeed);

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, currentTarget.localPosition + sineMovement, ref velocity, 0.5f);
    }

    private void TryDetach() {
        foreach (var partBreakable in partBreakables) {
            var isDetached = partBreakable.Detatch();
            if (isDetached) {
                TryDestroyBreakable();
                breakParticleSystem.Play();
                MoveTarget();
                break;
            }
        }
    }

    private void TryDestroyBreakable() {
        foreach (var partBreakable in partBreakables) {
            if (!partBreakable.GetIsDetached()) {
                return;
            }
        }

        breakParticleSystem.transform.SetParent(transform.parent);
        var emission = breakParticleSystem.main;
        emission.stopAction = ParticleSystemStopAction.Destroy;

        breakParticleSystem.Play();

        partReward.transform.SetParent(transform.parent);
        partReward.enabled = true;

        Destroy(gameObject);
    }

    private void MoveTarget() {
        var newTarget = GetRandomTarget();
        while (newTarget == currentTarget) {
            newTarget = GetRandomTarget();
        }

        currentTarget = newTarget;
    }

    private Transform GetRandomTarget() {
        var newTarget = breakableTargetsGroup.GetChild(GetRandomTargetIndex());
        return newTarget.transform;
    }

    private int GetRandomTargetIndex() {
        var count = breakableTargetsGroup.GetComponentsInChildren<Transform>().Length;
        return Random.Range(0, count-1);
    }
}
