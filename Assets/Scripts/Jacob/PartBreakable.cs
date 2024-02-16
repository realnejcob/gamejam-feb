using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBreakable : MonoBehaviour {
    private bool isDetached = false;
    private bool isColliding = false;
    private Vector3 triggerPosition = Vector3.zero;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Player")) {
            isColliding = true;
            triggerPosition = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.CompareTag("Player")) {
            isColliding = false;
        }
    }

    public bool Detatch() {
        //print(isColliding + " " + gameObject.name);

        if (isDetached)
            return false;

        if (!isColliding)
            return false;

        isDetached = true;

        transform.SetParent(null);
        GetComponent<Collider>().enabled = false;
        DisappearAnimation();
        return true;
    }

    private void DisappearAnimation() {
        var heading = triggerPosition - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        var dirNorm = direction.normalized;

        var distAmount = 2;

        LeanTween.scale(gameObject, Vector3.zero, 3);
        LeanTween.move(gameObject, transform.position - new Vector3(dirNorm.x * distAmount, dirNorm.y * distAmount, -5), 3)
            .setEaseOutQuint();
    }
}
