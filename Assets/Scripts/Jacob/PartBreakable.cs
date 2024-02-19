using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBreakable : MonoBehaviour {
    private bool isDetached = false;
    private bool isColliding = false;
    private Vector3 triggerPosition = Vector3.zero;

    [SerializeField] private float breakDistance = 2f;
    [SerializeField] private float breakTime = 4f;

    private Environment environment;

    private void Awake() {
        environment = GetComponentInParent<Environment>();
    }

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

        transform.SetParent(environment.GetObjectsTransform());
        GetComponent<Collider>().enabled = false;
        DisappearAnimation();
        return true;
    }

    private void DisappearAnimation() {
        var heading = triggerPosition - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        var dirNorm = direction.normalized;

        var tweens = new List<LTDescr>();
        tweens.Add(LeanTween.scale(gameObject, Vector3.zero, breakTime * 1.5f)
            .setEaseOutQuint()
            .setOnComplete(()=>gameObject.SetActive(false)));

        tweens.Add(LeanTween.moveLocal(gameObject, transform.position - new Vector3(dirNorm.x * breakDistance, dirNorm.y * breakDistance, 0), breakTime)
            .setEaseOutQuint());

        var randomAxis = new Vector3(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        tweens.Add(LeanTween.rotateAround(gameObject, randomAxis, 360, breakTime));
    }

    public bool GetIsDetached() {
        return isDetached;
    }
}
