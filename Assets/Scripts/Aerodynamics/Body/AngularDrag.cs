using UnityEngine;

public class AngularDrag : MonoBehaviour
{
    private Rigidbody rb;
    private AircraftSetup setup;

    private AngularDragConfig config => setup.config.angularDragConfig;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        setup = GetComponentInParent<AircraftSetup>();
    }

    private void FixedUpdate()
    {
        Vector3 localAngVel = transform.InverseTransformDirection(rb.angularVelocity);
        rb.AddRelativeTorque(
            config.GetAngularDrag(localAngVel, rb.velocity.magnitude),
            ForceMode.Force
        );
    }
}
