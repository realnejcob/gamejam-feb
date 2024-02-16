using System.Collections.Generic;
using UnityEngine;

public class PartAttractor : MonoBehaviour
{
    [SerializeField]
    GameObject SmallPartPrefab;

    [SerializeField]
    GameObject BigPartPrefab;

    [SerializeField]
    List<TriggerCollisionHelper> collisionHelpers = new List<TriggerCollisionHelper>();

    List<Part> registeredParts = new List<Part>();

    public List<Part> RegisteredParts => registeredParts;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < collisionHelpers.Count; i++)
            {
                TriggerCollisionHelper helper = collisionHelpers[i];

                // Check for trigger collision
                if (helper.IsTriggered == false)
                {
                    continue;
                }

                var other = helper.GetOtherCollider;

                Part part;
                part = other.transform.GetComponent<Part>();

                // Check for part script
                if (part == null)
                {
                    continue;
                }

                // Check that direction is correct
                float dot = Vector3.Dot(part.transform.forward, helper.transform.forward);
                if (dot < 0.8)
                {
                    continue;
                }

                // Check big vs small
                if (helper.tag == part.tag)
                {
                    part.SnappedEvent += RegisterPart;
                    part.Snap(helper.transform);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < collisionHelpers.Count; i++)
            {
                TriggerCollisionHelper helper = collisionHelpers[i];
                if (helper.tag == "Small")
                {
                    var smallPart = Instantiate(SmallPartPrefab).GetComponent<SmallPart>();
                    smallPart.Deactivate();
                    smallPart.SnappedEvent += RegisterPart;
                    smallPart.Snap(helper.transform);
                }
                else if (helper.tag == "Big")
                {
                    var bigPart = Instantiate(BigPartPrefab).GetComponent<BigPart>();
                    bigPart.Deactivate();
                    bigPart.SnappedEvent += RegisterPart;
                    bigPart.Snap(helper.transform);
                }
            }
        }
    }
    private void RegisterPart(Part part)
    {
        GetComponent<Machine>().PingRotation();

        registeredParts.Add(part);
    }

    public void UnRegisterParts()
    {
        registeredParts.Clear();
    }
}
