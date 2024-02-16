using UnityEngine;

public class MachineActivator : MonoBehaviour
{
    [SerializeField]
    Machine machine;

    [SerializeField]
    EnvironmentController environmentController;

    [SerializeField]
    PartAttractor partAttractor;

    private void Update()
    {
        if (environmentController.GetIsLookingUp())
        {
            var registeredParts = partAttractor.RegisteredParts;

            if (registeredParts.Count > 0)
            {
                //machine.SetFollowMouse(false);
            }
            else
            {
                // TODO: maybe feedback if you have not collected any parts yet? Negating audio clip?
            }
        }
    }
}
