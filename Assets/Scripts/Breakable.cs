using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float moveAmount = 1;
    [SerializeField] private float rotXSpeed = 1;
    [SerializeField] private float rotYSpeed = 1;
    [SerializeField] private float rotZSpeed = 1;

    [SerializeField] private Transform currentTarget;
    [SerializeField] private Transform breakableTargetsGroup;
    private Vector3 velocity;

    [Header("Parts:")]
    [SerializeField] private List<PartBreakable> partBreakables = new List<PartBreakable>();
    [SerializeField] private Part partReward;

    private Machine machine;

    private void Awake() {
        machine = FindObjectOfType<Machine>();
        machine.BreakAction += TryDetach;
    }

    private void Update() {
        var sineMovement = new Vector3(Mathf.Sin(Time.time * moveSpeed) * moveAmount, 0, 0);

        transform.Rotate(Vector3.right, rotXSpeed);
        transform.Rotate(Vector3.up, rotYSpeed);
        transform.Rotate(Vector3.forward, rotZSpeed);

        transform.localPosition = Vector3.SmoothDamp(transform.position, currentTarget.position + sineMovement, ref velocity, 0.5f);
    }

    private void TryDetach() {
        foreach (var partBreakable in partBreakables) {
            var isDetached = partBreakable.Detatch();
            if (isDetached) {
                TryDestroyBreakable();
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
        var newTarget = breakableTargetsGroup.GetComponentsInChildren<Transform>()[GetRandomTargetIndex()];
        return newTarget.transform;
    }

    private int GetRandomTargetIndex() {
        var count = breakableTargetsGroup.GetComponentsInChildren<Transform>().Length;
        return Random.Range(0, count-1);
    }
}
