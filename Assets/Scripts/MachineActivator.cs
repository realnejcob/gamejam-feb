using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class MachineActivator : MonoBehaviour
{
    [SerializeField]
    Machine machine;

    [SerializeField]
    EnvironmentController environmentController;

    [SerializeField]
    PartAttractor partAttractor;

    [SerializeField]
    float toDuration = 5;

    [SerializeField]
    float toSizeScalar = 0.1f;

    private void Start()
    {
        environmentController = FindObjectOfType<EnvironmentController>();
    }

    private void Update()
    {
        if (environmentController.GetIsLookingUp())
        {
            var registeredParts = partAttractor.RegisteredParts;

            if (registeredParts.Count > 0)
            {
                //machine.SetFollowMouse(false);
                ActivateMachine(registeredParts);
            }
            else
            {
                // TODO: maybe feedback if you have not collected any parts yet? Negating audio clip?
            }
        }
    }

    private void ActivateMachine(List<Part> registeredParts)
    {
        Sequence sequence = DOTween.Sequence();
        Vector3 toPosition = Vector3.zero;
        toPosition.z = machine.transform.position.z;
        sequence.Append(machine.transform.DOMove(toPosition, toDuration))
                .Join(machine.transform.DOScale(Vector3.one * toSizeScalar, toDuration))
                .Join(machine.transform.DORotate(Vector3.right * 90, toDuration));
    }

    private void EjectParts(List<Part> registeredParts)
    {
        // Re-parent all the parts
        // Spread them out lightly
        // Start from the first (or last?)
        // Call the Constellate function on the part
        // Something something connect the parts like stars
    }
}
