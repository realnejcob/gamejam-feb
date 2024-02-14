using UnityEngine;

public class PartAttractor : MonoBehaviour
{
    [SerializeField]
    Transform socketTransform;

    // TODO: Extend to detect the correct socket, perhaps collider?
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Hit: " + other.name);
            Part remote;
            remote = other.transform.root.GetComponent<Part>();
            print("Part: " + remote.name);
            if (remote != null)
            {
                print("Found part");
                remote.Snap(socketTransform);
            }
        }
    }
}
