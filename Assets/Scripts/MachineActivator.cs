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

    bool machineIsActivated = false;

    Sequence upSequence = null;

    private void Start()
    {
        environmentController = FindObjectOfType<EnvironmentController>();
    }

    private void Update()
    {
        if (environmentController.GetIsLookingUp())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (machineIsActivated)
                {
                    if (upSequence.IsPlaying())
                    {
                        upSequence.Kill();
                    }
                    DeactivateMachine();
                }
                else
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

        }
    }

    private void ActivateMachine(List<Part> registeredParts)
    {
        SetMachineActivationState(true);
        upSequence = DOTween.Sequence();
        Vector3 toPosition = Vector3.zero;
        toPosition.z = machine.transform.position.z;
        upSequence.Append(machine.transform.DOMove(toPosition, toDuration))
                .Join(machine.transform.DOScale(Vector3.one * toSizeScalar, toDuration))
                .Join(machine.transform.DORotate(Vector3.right * 90, toDuration));
    }

    private void DeactivateMachine()
    {
        Sequence downSequence = DOTween.Sequence();
        Vector3 toPosition = Vector3.zero;
        toPosition.z = machine.transform.position.z;
        downSequence.Append(machine.transform.DOMove(toPosition, toDuration))
                .Join(machine.transform.DOScale(Vector3.one, toDuration))
                .Join(machine.transform.DORotate(Vector3.zero, toDuration)).OnComplete(() => SetMachineActivationState(false));
    }


    void SetMachineActivationState(bool state)
    {
        machine.SetFollowMouse(!state);
        machineIsActivated = state;
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
