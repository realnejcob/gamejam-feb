using System.Collections.Generic;
using UnityEngine;

public class PartAttractor : MonoBehaviour
{
    [SerializeField]
    List<TriggerCollisionHelper> collisionHelpers = new List<TriggerCollisionHelper>();

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {

            for (int i = 0; i < collisionHelpers.Count; i++)
            {
                TriggerCollisionHelper helper = collisionHelpers[i];

                if (helper.IsTriggered)
                {
                    Part part;
                    part = other.transform.root.GetComponent<Part>();
                    print("Part: " + part.name);
                    if (part != null)
                    {
                        print("Found part");
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

            //print("Closest Transform: " + closestTransform.name);

            //Part part;
            //part = other.transform.root.GetComponent<Part>();
            //print("Part: " + part.name);
            //if (part != null)
            //{
            //    print("Found part");
            //    if (closestTransform.tag == "BigSocket" && part is BigPart)
            //    {
            //        part.Snap(closestTransform);
            //    }
            //    else if (closestTransform.tag == "SmallSocket" && part is SmallPart)
            //    {
            //        part.Snap(closestTransform);
            //    }
            //}
        }
    }
}
