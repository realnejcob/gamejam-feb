using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTestRotate : MonoBehaviour {

    private bool canTween = true;

    void Update()
    {
        CheckRotation();
    }

    private void CheckRotation() {
        if (!canTween)
            return;

        if (Input.GetKeyDown(KeyCode.W)) {
            DoRotation(Vector3.right * 90);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            DoRotation(Vector3.up * 90);
        } else if (Input.GetKeyDown(KeyCode.S)) {
            DoRotation(Vector3.left * 90);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            DoRotation(Vector3.down * 90);
        }
    }

    private void DoRotation(Vector3 _amount) {
        canTween = false;
        var _currentRotation = gameObject.transform.rotation.eulerAngles;
        LeanTween.rotate(gameObject, _currentRotation + _amount, 1)
            .setEaseOutQuint()
            .setOnComplete(() => canTween = true);
    }
}
