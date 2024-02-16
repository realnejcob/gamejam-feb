using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float moveAmount = 1;
    [SerializeField] private float rotXSpeed = 1;
    [SerializeField] private float rotYSpeed = 1;
    [SerializeField] private float rotZSpeed = 1;

    private void Update() {
        transform.localPosition = new Vector3(Mathf.Sin(Time.time * moveSpeed) * moveAmount, 0, transform.localPosition.z);
        transform.Rotate(Vector3.right, rotXSpeed);
        transform.Rotate(Vector3.up, rotYSpeed);
        transform.Rotate(Vector3.forward, rotZSpeed);
    }
}
