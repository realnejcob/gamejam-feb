using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {
    [SerializeField] private Camera cam;
    public Vector3 worldMouse;
    private Vector3 initPosition;

    private void Awake() {
        initPosition = transform.position;
    }

    private void Update() {
        var mousePos = Input.mousePosition;
        worldMouse = cam.ScreenToWorldPoint(mousePos);

        //gameObject.transform.position = initPosition + worldMouse;
    }
}
