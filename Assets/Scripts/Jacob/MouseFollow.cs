using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {
    [SerializeField] private Camera cam;
    private Vector3 worldMouse;
    private Vector3 initPosition;
    [SerializeField] private float smoothTime;
    private Vector3 velocity;

    private void Awake() {
        initPosition = transform.position;
    }

    private void Update() {
        var mousePos = Input.mousePosition;
        worldMouse = cam.ScreenToWorldPoint(mousePos);

        var target = initPosition + worldMouse;

        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, target, ref velocity, smoothTime);
    }
}
