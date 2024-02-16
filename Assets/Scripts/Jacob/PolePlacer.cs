using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolePlacer : MonoBehaviour {
    [SerializeField] private List<Transform> poles = new List<Transform>();
    private Camera cam;

    private void Awake() {
        cam = Camera.main;
        SetupPoles();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SetupPoles();
        }
    }

    private void SetupPoles() {
        foreach (var pole in poles) {
            pole.gameObject.SetActive(true);
        }

        var leftPos = cam.ScreenToWorldPoint(new Vector3(0, 0));
        var totalScreenWidthDist = (Mathf.Abs(leftPos.x) * 2) + 0.25f;
        poles[0].position = new Vector3(-totalScreenWidthDist / 2, poles[0].position.y, totalScreenWidthDist / 2);
        poles[1].position = new Vector3(totalScreenWidthDist / 2, poles[1].position.y, totalScreenWidthDist / 2);
        poles[2].position = new Vector3(-totalScreenWidthDist / 2, poles[2].position.y, -totalScreenWidthDist / 2);
        poles[3].position = new Vector3(totalScreenWidthDist / 2, poles[3].position.y, -totalScreenWidthDist / 2);
    }
}
