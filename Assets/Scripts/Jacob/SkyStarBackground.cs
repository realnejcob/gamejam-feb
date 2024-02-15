using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyStarBackground : MonoBehaviour {
    [SerializeField] private float speed = 10;
    public Transform meshTransform;

    private void Update() {
        meshTransform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
