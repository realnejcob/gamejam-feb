using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {
    [SerializeField] private Camera cam;
    public Vector2 worldMouse;

    private void Update() {
        var mousePos = Input.mousePosition;
        worldMouse = cam.ScreenToWorldPoint(mousePos);
    }
}
