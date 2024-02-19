using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {
    [SerializeField] private GameObject objectsGameObject;
    
    public void SetActive(bool active) {
        if (active) {
            objectsGameObject.transform.localPosition = Vector3.zero;
        } else {
            objectsGameObject.transform.localPosition = Vector3.forward * 5;
        }
    }

    public Transform GetObjectsTransform() {
        return objectsGameObject.transform;
    }
}
