using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float moveAmount = 1;
    [SerializeField] private float rotXSpeed = 1;
    [SerializeField] private float rotYSpeed = 1;
    [SerializeField] private float rotZSpeed = 1;

    [Header("Parts:")]
    [SerializeField] private List<PartBreakable> partBreakables = new List<PartBreakable>();

    private Machine machine;

    private void Awake() {
        machine = FindObjectOfType<Machine>();
        machine.BreakAction += TryDetach;
    }

    private void Update() {
        transform.localPosition = new Vector3(Mathf.Sin(Time.time * moveSpeed) * moveAmount, transform.localPosition.y, transform.localPosition.z);
        transform.Rotate(Vector3.right, rotXSpeed);
        transform.Rotate(Vector3.up, rotYSpeed);
        transform.Rotate(Vector3.forward, rotZSpeed);
    }

    private void TryDetach() {
        foreach (var partBreakable in partBreakables) {
            if (partBreakable.Detatch()) {
                break;
            }
        }
    }

    public void DetachPart(PartBreakable _partBreakable) {
        partBreakables.Remove(_partBreakable);
    }
}
