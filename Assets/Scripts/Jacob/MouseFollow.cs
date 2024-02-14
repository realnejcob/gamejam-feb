using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {
    [SerializeField] private Camera cam;
    private Vector3 worldMouse;
    private Vector3 initPosition;
    [SerializeField] private float smoothTime;
    public Vector3 velocity;

    private void Awake() {
        initPosition = transform.position;
        Cursor.visible = false;
    }

    private void Update() {
        var mousePos = Input.mousePosition;
        worldMouse = cam.ScreenToWorldPoint(mousePos);

        var target = initPosition + worldMouse;
        var newPosition = Vector3.SmoothDamp(gameObject.transform.position, target, ref velocity, smoothTime);

        gameObject.transform.position = newPosition;
    }
}
