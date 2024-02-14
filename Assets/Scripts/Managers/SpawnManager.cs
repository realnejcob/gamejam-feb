using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private SpawnedSwish switchObj;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(SpawnSwish());
        }
    }

    private IEnumerator SpawnSwish() {
        var _newSwitch = Instantiate(switchObj);
        var _randomPos = new Vector3(Random.Range(0.1f,-0.1f), Random.Range(0.1f, -0.1f), 5);
        _newSwitch.SetPosition(_randomPos);
        _newSwitch.InitializeObject();
        _newSwitch.DoSpawnAnimation();
        yield return null;
    }
}
