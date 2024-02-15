using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {
    private Camera cam;
    private Vector3 worldMouse;
    private Vector3 initPosition;
    [SerializeField] private float smoothTime;
    private Vector3 velocity;

    private bool canFollowDynamic = true;
    private Vector3 staticMousePos = Vector3.zero;

    private void Awake() {
        cam = Camera.main;
        initPosition = transform.position;
        Cursor.visible = false;
    }

    private void Update() {
        UpdateFollow();
    }

    private void UpdateFollow() {
        if (canFollowDynamic) {
            worldMouse = GetWorldMouse();
        } else {
            if (staticMousePos == Vector3.zero)
                staticMousePos = GetWorldMouse();
        }

        var target = initPosition + worldMouse;
        var newPosition = Vector3.SmoothDamp(gameObject.transform.position, target, ref velocity, smoothTime);

        gameObject.transform.position = newPosition;
    }

    public void SetCanFollowDynamic(bool _canFollow) {
        if (_canFollow) {
            staticMousePos = Vector3.zero;
        }

        canFollowDynamic = _canFollow;
    }

    private Vector3 GetWorldMouse() {
        var mousePos = Input.mousePosition;
        return cam.ScreenToWorldPoint(mousePos);
    }
}
