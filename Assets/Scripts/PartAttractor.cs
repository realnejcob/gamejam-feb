using System.Collections.Generic;
using UnityEngine;

public class PartAttractor : MonoBehaviour
{
    [SerializeField]
    List<TriggerCollisionHelper> collisionHelpers = new List<TriggerCollisionHelper>();

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
                part = other.transform.root.GetComponent<Part>();

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
                if (helper.tag == "BigSocket" && part is BigPart)
                {
                    part.Snap(helper.transform);
                }
                else if (helper.tag == "SmallSocket" && part is SmallPart)
                {
                    part.Snap(helper.transform);
                }
            }
        }
    }
}
