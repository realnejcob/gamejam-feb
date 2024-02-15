using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyStar : MonoBehaviour {
    private MeshRenderer starMesh;

    private void Awake() {
        starMesh = GetComponentInChildren<MeshRenderer>();
        starMesh.material.SetFloat("_Seed", Random.Range(0, 30000));
        starMesh.material.SetFloat("_FadeSpeed", Random.Range(0.1f, 1f));
        starMesh.material.SetColor("_MainColor", Color.Lerp(Color.white, Color.grey, Random.Range(0f,1f)) * 2);
    }
}
